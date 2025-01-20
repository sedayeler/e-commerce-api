using ECommerceAPI.Application.DTOs.Basket;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetBasketItemsAsync();
        Task AddItemToBasketAsync(CreateBasketItemRequest request);
        Task UpdateQuantityAsync(UpdateBasketItemRequest request);
        Task RemoveBasketItemAsync(int basketItemId);
    }
}
