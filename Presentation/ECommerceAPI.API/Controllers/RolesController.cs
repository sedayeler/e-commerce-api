using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.Role.CreateRole;
using ECommerceAPI.Application.Features.Commands.Role.DeleteRole;
using ECommerceAPI.Application.Features.Commands.Role.UpdateRole;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.Role.GetAllRoles;
using ECommerceAPI.Application.Features.Queries.Role.GetRoleById;
using ECommerceAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.roles, ActionType = ActionType.Reading, Definition = "Get all roles")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRolesQueryRequest request)
        {
            List<GetAllRolesQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.roles, ActionType = ActionType.Reading, Definition = "Get role by id")]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQueryRequest request)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.roles, ActionType = ActionType.Writing, Definition = "Create role")]
        public async Task<IActionResult> CreateRole(CreateRoleCommandRequest request)
        {
            CreateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.roles, ActionType = ActionType.Updating, Definition = "Update role")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommandRequest request)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.roles, ActionType = ActionType.Deleting, Definition = "Delete role")]
        public async Task<IActionResult> DeleteRole(DeleteRoleCommandRequest request)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
