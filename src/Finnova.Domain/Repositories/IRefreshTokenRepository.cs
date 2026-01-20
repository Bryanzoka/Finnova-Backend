using Finnova.Domain.Entities;

namespace Finnova.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<List<RefreshToken>?> GetAllByUserIdAsync(int userId, bool active = true);
        Task AddAsync(RefreshToken refreshToken);
        void Update(RefreshToken refreshToken);
        void Delete(RefreshToken refreshToken);
    }
}