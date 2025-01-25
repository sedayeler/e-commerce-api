using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        int TotalUsersCount { get; }
        Task<List<ListUser>> GetAllUsersAsync();
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest userRequest);
        Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<List<string>> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
