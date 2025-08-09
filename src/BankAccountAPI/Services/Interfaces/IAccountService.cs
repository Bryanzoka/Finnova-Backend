using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using BankAccountAPI.Models.DTOs.Account;

namespace BankAccountAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<BankAccountModel>> SearchAllAccounts();
        Task<BankAccountDTO> SearchAccountById(int id);
        Task<List<AccountPreviewDTO>> SearchAllAccountsByCpf(string cpf);
        Task<BankAccountDTO> AddAccount(CreateAccountDTO account);  
        Task<BankAccountDTO> DepositBalance(DepositDTO deposit, string clientCpf);
        Task<BankAccountDTO> WithdrawBalance(WithdrawDTO withdraw, string clientCpf);
        Task<BankAccountDTO> TransferBalance(decimal transfer, int accountId, int recipientId, string clientCpf);
        Task<bool> DeleteAccount(int id, string clientCpf);
    }
}