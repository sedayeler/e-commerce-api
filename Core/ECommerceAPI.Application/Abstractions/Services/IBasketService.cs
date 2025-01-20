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
        Task<Basket> GetUserBasketAsync();
        Task AddItemToBasketAsync(CreateBasketItemRequest request);
        Task<List<BasketItem>> GetBasketItemAsync();
        Task UpdateQuantityAsync(UpdateBasketItemRequest request);
        Task RemoveBasketItemAsync(int basketItemId);                              
    }
}
