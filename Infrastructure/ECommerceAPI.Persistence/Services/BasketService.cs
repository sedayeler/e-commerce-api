﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Basket;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public BasketService(HttpContextAccessor httpContextAccessor, UserManager<User> userManager, IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository)
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
                throw new Exception("User information could not be retrieved.");

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

        public async Task<Basket> GetUserBasketAsync()
        {
            Basket basket = await UserBasket();
            return basket;
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
                    Id = basket.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                await _basketItemWriteRepository.AddAsync(newItem);
            }
            await _basketItemWriteRepository.SaveAsync();
        }

        public async Task<List<BasketItem>> GetBasketItemAsync()
        {
            Basket basket = await UserBasket();

            Basket? result = await _basketReadRepository.Table
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.ProductId)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
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
