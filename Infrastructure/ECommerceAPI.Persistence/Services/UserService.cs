using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<User> userManager, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
        }

        public int TotalUsersCount => _userManager.Users.Count();

        public async Task<List<ListUser>> GetAllUsersAsync()
        {
            var user = await _userManager.Users.ToListAsync();

            return user.Select(u => new ListUser
            {
                Id = u.Id,
                FullName = u.FullName,
                Username = u.UserName,
                Email = u.Email,
                TwoFactorEnabled = u.TwoFactorEnabled
            }).ToList();
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest userRequest)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = userRequest.FullName,
                UserName = userRequest.Username,
                Email = userRequest.Email
            }, userRequest.Password);

            CreateUserResponse userResponse = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                userResponse.Message = "User registration successful.";
            }
            else
            {
                foreach (var error in result.Errors)
                    userResponse.Message = $"{error.Code} - {error.Description}";
            }

            return userResponse;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");
            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<List<string>> GetRolesToUserAsync(string userIdOrName)
        {
            var user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user == null)
                throw new Exception("User not found.");
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToList();
            }
            return new List<string> { };
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null) return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                {
                    if (userRole == endpointRole)
                        return true;
                }
            }
            return false;
        }
    }
}
