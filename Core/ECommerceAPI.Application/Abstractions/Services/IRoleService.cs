using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> UpdateRoleAsync(string id, string name);
        Task<bool> DeleteRoleAsync(string id);
    }
}
