using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Endpoint.AssignRoleToEndpoint
{
    public class AssignRoleToEndpointCommandHandler : IRequestHandler<AssignRoleToEndpointCommandRequest, AssignRoleToEndpointCommandResponse>
    {
        private readonly IAuthEndpointService _authEndpointService;

        public AssignRoleToEndpointCommandHandler(IAuthEndpointService authEndpointService)
        {
            _authEndpointService = authEndpointService;
        }

        public async Task<AssignRoleToEndpointCommandResponse> Handle(AssignRoleToEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await _authEndpointService.AssignRoleToEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);

            return new();
        }
    }
}
