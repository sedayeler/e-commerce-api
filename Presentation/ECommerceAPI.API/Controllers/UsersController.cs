using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.User.AssignRoleToUser;
using ECommerceAPI.Application.Features.Commands.User.CreateUser;
using ECommerceAPI.Application.Features.Commands.User.LoginUser;
using ECommerceAPI.Application.Features.Commands.User.RefreshToken;
using ECommerceAPI.Application.Features.Queries.User.GetAllUsers;
using ECommerceAPI.Application.Features.Queries.User.GetRolesToUser;
using ECommerceAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.users, ActionType = ActionType.Reading, Definition = "Get All Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(request);
            return Ok(response);
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
        public async Task<IActionResult> RefreshTokenLoginUser([FromForm] RefreshTokenCommandRequest request)
        {
            RefreshTokenCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.users, ActionType = ActionType.Writing, Definition = "Assign Role To User")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest request)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.users, ActionType = ActionType.Reading, Definition = "Get Roles To User")]
        public async Task<IActionResult> GetRolesToUser(GetRolesToUserQueryRequest request)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
