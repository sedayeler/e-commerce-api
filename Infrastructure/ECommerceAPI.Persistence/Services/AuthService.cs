using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Features.Commands.User.LoginUser;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> LoginAsync(string UsernameOrEmail, string Password, int accessTokenLifetime)
        {
            User user = await _userManager.FindByNameAsync(UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(UsernameOrEmail);
            }
            if (user == null)
            {
                throw new Exception("Username or password is incorrect.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, Password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifetime);
                return token;
            }
            throw new Exception("Username or password is incorrect.");
        }
    }
}
