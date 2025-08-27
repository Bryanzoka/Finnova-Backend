using FinnovaAPI.Models;
using FinnovaAPI.Data;
using Microsoft.EntityFrameworkCore;
using FinnovaAPI.Repositories.Interfaces;

namespace FinnovaAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinnovaDbContext _dbContext;
        public AccountRepository(FinnovaDbContext finnovaDbContext)
        {
            _dbContext = finnovaDbContext;
        }

        public async Task<List<BankAccountModel>> SearchAllAccounts()
        {
           return await _dbContext.BankAccount.Include(x => x.BankClient).ToListAsync(); 
        }

        public async Task<BankAccountModel> SearchAccountById(int id)
        {
            BankAccountModel accountById = await _dbContext.BankAccount.FirstOrDefaultAsync(c => c.Id == id);

            return accountById;
        }

        public async Task<List<BankAccountModel>> SearchAllAccountsByClientId(int clientId)
        {
            return await _dbContext.BankAccount.Where(x => x.ClientId == clientId).ToListAsync();
        }

        public async Task<BankAccountModel> AddAccount(BankAccountModel account)
        {
            await _dbContext.BankAccount.AddAsync(account);
            await _dbContext.SaveChangesAsync();

            return account;
        }

        public async Task<BankAccountModel> UpdateBalance(BankAccountModel account)
        {
            _dbContext.BankAccount.Update(account);
            await _dbContext.SaveChangesAsync();

            return account;
        }

        public async Task<bool> DeleteAccount(BankAccountModel account)
        {
            _dbContext.BankAccount.Remove(account);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}