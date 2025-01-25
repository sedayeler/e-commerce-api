using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute, User user);
        string CreateRefreshToken();
    }
}
