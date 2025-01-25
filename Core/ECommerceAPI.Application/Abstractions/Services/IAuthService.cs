namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
