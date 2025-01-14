using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.User> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.User> _signInManager;
        private readonly ITokenHandler tokenHandler;

        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.User> userManager, SignInManager<Domain.Entities.Identity.User> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.User user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }

            if (user == null)
            {
                throw new Exception("Username or password is incorrect.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                Token token = tokenHandler.CreateAccessToken(10);
                return new LoginUserCommandResponse()
                {
                    Token = token
                };
            }

            return new LoginUserCommandResponse()
            {
                Message = "Username or password is incorrect."
            };
        }
    }
}
