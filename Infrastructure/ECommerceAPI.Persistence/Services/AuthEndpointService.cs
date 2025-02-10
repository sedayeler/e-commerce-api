using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class AuthEndpointService : IAuthEndpointService
    {
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriteRepository _endpointWriteRepository;
        private readonly IAppService _appService;
        private readonly RoleManager<Role> _roleManager;

        public AuthEndpointService(IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IAppService appService, RoleManager<Role> roleManager)
        {
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _appService = appService;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Name = menu
                };

                await _menuWriteRepository.AddAsync(_menu);
                await _menuWriteRepository.SaveAsync();
            }


            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Menu)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Menu.Name == menu && e.Code == code);

            if (endpoint == null)
            {
                var action = _appService.GetAuthorizeDefinitionEndpoints(type)
                    .FirstOrDefault(m => m.Name == menu)?
                    .Actions.FirstOrDefault(a => a.Code == code);

                endpoint = new()
                {
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Code = code,
                    Menu = _menu
                };

                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            var _roles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in _roles)
                endpoint.Roles.Add(role);

            await _endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string menu, string code)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Menu)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Menu.Name == menu && e.Code == code);

            if (endpoint != null)
                return endpoint.Roles.Select(r => r.Name).ToList();

            return null;
        }
    }
}
