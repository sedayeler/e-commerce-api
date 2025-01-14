using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Features.Commands.User.CreateUser;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
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
    }
}
