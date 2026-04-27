using Product_API.Repository.Interface;
using Product_API.Models;
using Product_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Product_API.Repository
{
    public class TokenRepository : ITokenRepository
    {

        private readonly AppDbContext _dbContext;

        public TokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveTokenAsync(RefreshToken refreshToken)
        {
            var existingToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(r=>r.UserId==refreshToken.UserId);

            if (existingToken != null)
            {
                existingToken.UserId = refreshToken.UserId;                 
                existingToken.Expiry = refreshToken.Expiry;                 
            }
            
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
             
            await _dbContext.SaveChangesAsync();
        }
        public async Task<RefreshToken?> GetTokenAsync(string userId)
        {
            return await _dbContext.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(r=>r.UserId==userId);
            
        }
    }
}