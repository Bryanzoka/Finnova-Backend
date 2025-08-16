using FinnovaAPI.Models;
using FinnovaAPI.Models.DTOs.Account;

namespace FinnovaAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<BankAccountModel>> SearchAllAccounts();
        Task<BankAccountDTO> SearchAccountById(int id);
        Task<List<AccountPreviewDTO>> SearchAllAccountsByClientId(int id);
        Task<BankAccountDTO> AddAccount(CreateAccountDTO account);  
        Task<BankAccountDTO> DepositBalance(DepositDTO deposit, int clientId);
        Task<BankAccountDTO> WithdrawBalance(WithdrawDTO withdraw, int clientId);
        Task<BankAccountDTO> TransferBalance(decimal transfer, int accountId, int recipientId, int clientId);
        Task<bool> DeleteAccount(int id, int clientId);
    }
}