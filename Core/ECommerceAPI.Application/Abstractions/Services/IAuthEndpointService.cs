namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthEndpointService
    {
        Task AssignRoleToEndpointAsync(string[] roles, string menu, string code, Type type);
        Task<List<string>> GetRolesToEndpointAsync(string menu, string code);
    }
}
