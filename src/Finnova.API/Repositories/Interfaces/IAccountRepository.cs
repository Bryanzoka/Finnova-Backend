using FinnovaAPI.Models;

namespace FinnovaAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<BankAccountModel>> SearchAllAccounts();
        Task<BankAccountModel> SearchAccountById(int id);
        Task<List<BankAccountModel>> SearchAllAccountsByClientId(int id);
        Task<BankAccountModel> AddAccount(BankAccountModel account);  
        Task<BankAccountModel> UpdateAccount(BankAccountModel account);
        Task<bool> DeleteAccount(BankAccountModel account);
    }
}