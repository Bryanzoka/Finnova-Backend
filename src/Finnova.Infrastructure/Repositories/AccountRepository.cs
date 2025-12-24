using Finnova.Domain.Entities;
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

        public async Task<Account?> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<List<Account>?> GetAllByUserIdAsync(int userId)
        {
            return await _dbContext.Accounts.Where(a => a.UserId == userId).ToListAsync();
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