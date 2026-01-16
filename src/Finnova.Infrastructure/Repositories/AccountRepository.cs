using Finnova.Domain.Entities;
using Finnova.Domain.Enums;
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

        public async Task<List<Account>?> GetAllByUserIdAsync(int userId, bool? isActive, AccountType? type, decimal? minBalance, decimal? maxBalance)
        {
            var accounts = _dbContext.Accounts.AsQueryable().Where(a => a.UserId == userId);

            if (isActive.HasValue)
            {
                accounts = accounts.Where(a => a.IsActive == isActive);
            }

            if (type.HasValue)
            {
                accounts = accounts.Where(a => a.Type == type);
            }

            if (minBalance.HasValue)
            {
                accounts = accounts.Where(a => a.Balance >= minBalance);
            }

            if (maxBalance.HasValue)
            {
                accounts = accounts.Where(a => a.Balance <= maxBalance);
            }

            return await accounts.ToListAsync();
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