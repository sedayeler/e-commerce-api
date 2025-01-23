using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Endpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
    {
        private readonly IAuthEndpointService _authEndpointService;

        public GetRolesToEndpointQueryHandler(IAuthEndpointService authEndpointService)
        {
            _authEndpointService = authEndpointService;
        }

        public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            List<string> roles = await _authEndpointService.GetRolesToEndpointAsync(request.Menu, request.Code);

            return new()
            {
                Roles = roles
            };
        }
    }
}
