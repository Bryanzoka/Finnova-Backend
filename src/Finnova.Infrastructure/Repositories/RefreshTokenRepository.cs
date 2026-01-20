using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using Finnova.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public RefreshTokenRepository(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;    
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task<List<RefreshToken>?> GetAllByUserIdAsync(int userId, bool active = true)
        {
            return await _dbContext.RefreshTokens.Where(r => r.UserId == userId && r.IsActive == active).ToListAsync();
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
        }

        public void Update(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Update(refreshToken);
        }

        public void Delete(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Remove(refreshToken);
        }
    }
}