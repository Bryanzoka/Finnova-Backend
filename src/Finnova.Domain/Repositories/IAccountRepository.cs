using Finnova.Domain.Entities;
using Finnova.Domain.Enums;

namespace Finnova.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>?> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByUserIdAsync(int userId); 
        Task<List<Account>?> GetAllByUserIdAsync(int userId, bool? isActive, AccountType? type, decimal? minBalance, decimal? maxBalance);
        Task AddAsync(Account account);
        void Update(Account account);
        void Delete(Account account);
    }
}