using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<List<ListBasketItem>> GetBasketItemsAsync();
        Task AddItemToBasketAsync(CreateBasketItemRequest request);
        Task UpdateQuantityAsync(UpdateBasketItemRequest request);
        Task RemoveBasketItemAsync(int basketItemId);
    }
}
