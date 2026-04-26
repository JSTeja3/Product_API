using Product_API.Models;

namespace Product_API.Repository.Interface
{
    public interface ITokenRepository
    {
        Task SaveTokenAsync(RefreshToken refreshTtoken);
        
        Task<RefreshToken?> GetTokenAsync(string userId);
    }
}