using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _basketReadRepository = basketReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        private async Task<Basket> UserBasket()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User authentication error.");

            User? user = await _userManager.Users
                .Include(u => u.Basket)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
                throw new Exception("User not found.");

            Basket basket = user.Basket;
            if (basket == null)
            {
                basket = new() { UserId = user.Id };
                await _basketWriteRepository.AddAsync(basket);
                await _basketWriteRepository.SaveAsync();
            }
            return basket;
        }

        public async Task<List<ListBasketItem>> GetBasketItemsAsync()
        {
            Basket basket = await UserBasket();

            Basket? result = await _basketReadRepository.Table
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.Select(bi => new ListBasketItem
            {
                Id = bi.Id,
                Quantity = bi.Quantity,
                BasketId = bi.BasketId,
                ProductId = bi.ProductId,
                Product = bi.Product
            }).ToList();
        }

        public async Task AddItemToBasketAsync(CreateBasketItemRequest request)
        {
            Basket basket = await UserBasket();

            BasketItem existingItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == request.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var newItem = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                await _basketItemWriteRepository.AddAsync(newItem);
            }
            await _basketItemWriteRepository.SaveAsync();
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemRequest request)
        {
            BasketItem existingItem = await _basketItemReadRepository.GetByIdAsync(request.BasketItemId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task RemoveBasketItemAsync(int basketItemId)
        {
            BasketItem existingItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (existingItem != null)
            {
                _basketItemWriteRepository.Remove(existingItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
