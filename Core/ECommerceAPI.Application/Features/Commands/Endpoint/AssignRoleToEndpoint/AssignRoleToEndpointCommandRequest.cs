using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Endpoint.AssignRoleToEndpoint
{
    public class AssignRoleToEndpointCommandRequest : IRequest<AssignRoleToEndpointCommandResponse>
    {
        public string[] Roles { get; set; }
        public string Menu { get; set; }
        public string Code { get; set; }
        public Type? Type { get; set; }
    }
}
