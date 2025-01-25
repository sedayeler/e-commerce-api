using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        int TotalUsersCount { get; }
        Task<List<ListUser>> GetAllUsersAsync();
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest userRequest);
        Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<List<string>> GetRolesToUserAsync(string userId);
    }
}
