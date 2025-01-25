using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetUserOrdersAsync();
        Task<List<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(CreateOrderRequest request);
        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request);
    }
}
