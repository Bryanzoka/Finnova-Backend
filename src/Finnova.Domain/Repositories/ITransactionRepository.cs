
using Finnova.Domain.Entities;
using Finnova.Domain.Enums;

namespace Finnova.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>?> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task<List<Transaction>?> GetAllByAccountIdAsync(int accountId, TransactionType? type, decimal? minAmount, decimal? maxAmount, DateTime? startDate, DateTime? endDate);
        Task<List<Transaction>?> GetAllByUserIdAsync(int userId, TransactionType? type, decimal? minAmount, decimal? maxAmount, DateTime? startDate, DateTime? endDate);
        Task AddAsync(Transaction transaction);
    }
}