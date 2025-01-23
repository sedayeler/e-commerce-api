using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryRequest : IRequest<List<GetAllRolesQueryResponse>>
    {
    }
}
