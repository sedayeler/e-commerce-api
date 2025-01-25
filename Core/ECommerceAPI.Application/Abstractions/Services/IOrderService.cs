using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<List<ListOrder>> GetUserOrdersAsync();
        Task<List<ListOrder>> GetAllOrdersAsync();
        Task CreateOrderAsync(CreateOrderRequest request);
        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request);
    }
}
