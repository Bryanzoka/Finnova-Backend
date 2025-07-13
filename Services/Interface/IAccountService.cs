using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using BankAccountAPI.Models.DTOs;

namespace BankAccountAPI.Services.Interface
{
    public interface IAccountService
    {
        Task<List<BankAccountModel>> SearchAllAccounts();
        Task<BankAccountDTO> SearchAccountById(int id);
        Task<BankAccountModel> AddAccount(CreateAccountDTO account);  
        Task<BankAccountDTO> DepositBalance(decimal deposit, int id);
        Task<BankAccountModel> WithdrawBalance(decimal withdraw, int id);
        Task<BankAccountModel> TransferBalance(decimal transfer, int accountId, int recipientId);
        Task<bool> DeleteAccount(int id);
    }
}