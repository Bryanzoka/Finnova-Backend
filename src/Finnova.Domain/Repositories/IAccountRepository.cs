using Finnova.Domain.Entities;

namespace Finnova.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>?> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByUserIdAsync(int userId);
        Task<List<Account>?> GetAllByUserIdAsync(int userId);
        Task AddAsync(Account account);
        void Update(Account account);
        void Delete(Account account);
    }
}