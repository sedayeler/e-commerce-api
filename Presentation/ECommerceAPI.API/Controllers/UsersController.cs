﻿using ECommerceAPI.Application.Features.Commands.User.CreateUser;
using ECommerceAPI.Application.Features.Commands.User.LoginUser;
using ECommerceAPI.Application.Features.Commands.User.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLoginUser([FromForm] RefreshTokenRequest request)
        {
            RefreshTokenResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
