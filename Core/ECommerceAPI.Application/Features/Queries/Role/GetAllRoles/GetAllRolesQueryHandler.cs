using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, List<GetAllRolesQueryResponse>>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            List<ListRole> roles = await _roleService.GetAllRolesAsync();

            return roles.Select(role => new GetAllRolesQueryResponse
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }
    }
}
