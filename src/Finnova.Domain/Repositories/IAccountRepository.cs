using Finnova.Domain.Aggregates;

namespace Finnova.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>?> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByClientIdAsync(int clientId);
        Task<IReadOnlyList<Account>?> GetAllByClientIdAsync(int clientId);
        Task AddAsync(Account account);
        void Update(Account account);
        void Delete(Account account);
    }
}