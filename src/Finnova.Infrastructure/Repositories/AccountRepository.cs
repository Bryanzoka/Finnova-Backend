using Finnova.Domain.Aggregates;
using Finnova.Domain.Repositories;
using Finnova.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public AccountRepository(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Account>?> GetAllAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account?> GetByClientIdAsync(int clientId)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ClientId == clientId);
        }

        public async Task<IReadOnlyList<Account>?> GetAllByClientIdAsync(int clientId)
        {
            return await _dbContext.Accounts.Where(a => a.ClientId == clientId).ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
        }

        public void Update(Account account)
        {
            _dbContext.Accounts.Update(account);
        }

        public void Delete(Account account)
        {
            _dbContext.Accounts.Remove(account);
        }
    }
}