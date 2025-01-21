using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Enums;
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
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        private string GenerateOrderNumber()
        {
            Random random = new Random();
            return random.Next(1, 99999).ToString();
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User authentication error.");

            User? user = await _userManager.Users
                .Include(u => u.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user?.Basket == null || !user.Basket.BasketItems.Any())
                throw new Exception("Your basket is empty.");

            Order order = new()
            {
                OrderNumber = GenerateOrderNumber(),
                Address = request.Address,
                Status = OrderStatus.Preparing.ToString(),
                TotalPrice = user.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserId = user.Id,
                OrderItems = user.Basket.BasketItems.Select(bi => new OrderItem()
                {
                    ProductId = bi.ProductId,
                    Quantity = bi.Quantity,
                    Price = bi.Product.Price
                }).ToList()
            };

            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<List<Order>> GetUserOrdersAsync()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User authentication error.");

            User? user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("User not found.");

            return await _orderReadRepository.Table
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderReadRepository.Table
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
        {
            Order order = await _orderReadRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new Exception("Order not found.");
            order.Status = request.OrderStatus.ToString();
            _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();
            return true;
        }
    }
}
