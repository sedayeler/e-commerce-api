﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IMailService _mailService;

        public OrderService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IMailService mailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _mailService = mailService;
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

            string subject = "Order Information";
            string body = $"Dear {user.FullName},<p>Your order with number {order.OrderNumber} has been successfully created.</p>";

            await _mailService.SendMailAsync(user.Email, subject, body);

            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<List<ListOrder>> GetUserOrdersAsync()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User authentication error.");

            User? user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("User not found.");

            var order = await _orderReadRepository.Table
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return order.Select(o => new ListOrder()
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Address = o.Address,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                UserId = o.UserId,
                OrderItems = o.OrderItems
            }).ToList();
        }

        public async Task<List<ListOrder>> GetAllOrdersAsync()
        {
            var orders = await _orderReadRepository.Table
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return orders.Select(o => new ListOrder()
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Address = o.Address,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                UserId = o.UserId,
                OrderItems = o.OrderItems
            }).ToList();
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
