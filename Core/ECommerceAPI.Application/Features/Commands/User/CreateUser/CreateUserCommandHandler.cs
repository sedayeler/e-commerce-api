using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse userResponse = await _userService.CreateUserAsync(new()
            {
                FullName = request.FullName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            });

            return new()
            {
                Succeeded = userResponse.Succeeded,
                Message = userResponse.Message
            };
        }
    }
}
