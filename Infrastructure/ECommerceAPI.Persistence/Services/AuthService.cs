using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService _userService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
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
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifetime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 20);
                return token;
            }
            else
            {
                throw new Exception("Username or password is incorrect.");
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow.AddHours(3))
            {
                Token token = _tokenHandler.CreateAccessToken(10, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 20);
                return token;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
