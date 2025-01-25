using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAppService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
