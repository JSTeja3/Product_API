
namespace Product_API.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId);
        Task<string> GenerateRefreshTokenandSaveAsync(string userId);
        Task<bool> ValidateRefreshToken(string userId, string token);
    }
}