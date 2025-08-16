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

        public async Task<BankAccountModel> DepositBalance(decimal deposit, int id)
        {
            BankAccountModel accountById = await SearchAccountById(id);
            accountById.Deposit(deposit);

            _dbContext.BankAccount.Update(accountById);
            await _dbContext.SaveChangesAsync();

            return accountById;
        }

        public async Task<BankAccountModel> WithdrawBalance(decimal withdraw, int id)
        {
            BankAccountModel accountById = await SearchAccountById(id);
            accountById.Withdraw(withdraw);

            _dbContext.BankAccount.Update(accountById);
            await _dbContext.SaveChangesAsync();

            return accountById;
        }

        public async Task<BankAccountModel> TransferBalance(decimal transfer, int accountId, int recipientId)
        {
            BankAccountModel accountById = await SearchAccountById(accountId);
            BankAccountModel recipientById = await SearchAccountById(recipientId);
            accountById.Withdraw(transfer);
            recipientById.Deposit(transfer);

            _dbContext.BankAccount.Update(accountById);
            _dbContext.BankAccount.Update(recipientById);
            await _dbContext.SaveChangesAsync();

            return accountById;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            BankAccountModel accountById = await SearchAccountById(id);

            _dbContext.BankAccount.Remove(accountById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}