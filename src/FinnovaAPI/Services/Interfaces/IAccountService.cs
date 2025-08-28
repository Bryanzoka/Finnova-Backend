using FinnovaAPI.Models;
using FinnovaAPI.Models.DTOs.Account;

namespace FinnovaAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<BankAccountModel>> SearchAllAccounts();
        Task<BankAccountDTO> GetAuthenticatedClientAccount(int id, int clientId);
        Task<BankAccountDTO> SearchAccountById(int id);
        Task<List<AccountPreviewDTO>> SearchAllAccountsByClientId(int id);
        Task<BankAccountDTO> AddAccount(CreateAccountDTO account, int clientId);
        Task<BankAccountDTO> UpdateAccount(UpdateAccountDTO account, int clientId);
        Task<BankAccountDTO> DepositBalance(DepositDTO deposit, int clientId);
        Task<BankAccountDTO> WithdrawBalance(WithdrawDTO withdraw, int clientId);
        Task<BankAccountDTO> TransferBalance(TransferDTO transfer, int clientId);
        Task<bool> DeleteAccount(int id, int clientId);
    }
}