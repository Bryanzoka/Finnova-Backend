using FinnovaAPI.Models;

namespace FinnovaAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<BankAccountModel>> SearchAllAccounts();
        Task<BankAccountModel> SearchAccountById(int id);
        Task<List<BankAccountModel>> SearchAllAccountsByCpf(string cpf);
        Task<BankAccountModel> AddAccount(BankAccountModel account);  
        Task<BankAccountModel> DepositBalance(decimal deposit, int id);
        Task<BankAccountModel> WithdrawBalance(decimal withdraw, int id);
        Task<BankAccountModel> TransferBalance(decimal transfer, int accountId, int recipientId);
        Task<bool> DeleteAccount(int id);
    }
}