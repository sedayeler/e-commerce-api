using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<List<ListRole>> GetAllRolesAsync();
        Task<ListRole> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> UpdateRoleAsync(string id, string name);
        Task<bool> DeleteRoleAsync(string id);
    }
}
