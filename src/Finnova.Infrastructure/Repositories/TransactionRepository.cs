using Finnova.Domain.Entities;
using Finnova.Domain.Enums;
using Finnova.Domain.Repositories;
using Finnova.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public TransactionRepository(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>?> GetAllAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Transaction>?> GetAllByAccountIdAsync(int accountId, TransactionType? type, decimal? minAmount, decimal? maxAmount, DateTime? startDate, DateTime? endDate)
        {
            var transactions = _dbContext.Transactions.AsQueryable().Where(t => t.AccountId == accountId);

            if (type.HasValue)
            {
                transactions = transactions.Where(t => t.Type == type);
            }

            if (minAmount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount >= minAmount);
            }

            if (maxAmount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount <= maxAmount);
            }

            if (startDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date >= startDate);
            }

            if (startDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date <= endDate);
            }

            return await transactions.ToListAsync();
        }

        public async Task<List<Transaction>?> GetAllByUserIdAsync(int userId, TransactionType? type, decimal? minAmount, decimal? maxAmount, DateTime? startDate, DateTime? endDate)
        {
            var transactions = _dbContext.Transactions.Where(t => _dbContext.Accounts.Where(a => a.UserId == userId).Select(a => a.Id).Contains(t.AccountId));

            if (type.HasValue)
            {
                transactions = transactions.Where(t => t.Type == type);
            }

            if (minAmount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount >= minAmount);
            }

            if (maxAmount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount <= maxAmount);
            }

            if (startDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date >= startDate);
            }

            if (startDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date <= endDate);
            }

            return await transactions.ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
        }
    }
}