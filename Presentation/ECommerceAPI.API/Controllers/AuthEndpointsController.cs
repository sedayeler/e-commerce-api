using ECommerceAPI.Application.Features.Commands.Endpoint.AssignRoleToEndpoint;
using ECommerceAPI.Application.Features.Queries.Endpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/auth-endpoints")]
    [ApiController]
    public class AuthEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest request)
        {
            GetRolesToEndpointQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToEndpoint(AssignRoleToEndpointCommandRequest request)
        {
            request.Type = typeof(Program);
            AssignRoleToEndpointCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
