using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
using BankAccountAPI.Repository;
using BankAccountAPI.Enums;
using BankAccountAPI.Models.DTOs;
using System.ComponentModel;
using Microsoft.VisualBasic;

namespace BankAccountAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientService _clientServices;

        public AccountService(IAccountRepository accountRepository, IClientService clientServices)
        {
            _accountRepository = accountRepository;
            _clientServices = clientServices;
        }
        public async Task<List<BankAccountModel>> SearchAllAccounts()
        {
            return await _accountRepository.SearchAllAccounts();
        }

        public async Task<BankAccountDTO> SearchAccountById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"The ID: {id} is invalid");
            }

            BankAccountModel accountById = await _accountRepository.SearchAccountById(id) ?? throw new KeyNotFoundException($"Account not found");

            return BankAccountDTO.ToDTO(accountById);
        }

        public async Task<BankAccountDTO> AddAccount(CreateAccountDTO account)
        {
            // Validate CPF and ensure client exists
            await _clientServices.SearchClientByCPF(account.CPF);

            /* if (account.AccountType != EnumAccountType.Current && account.AccountType != EnumAccountType.Savings)
            {
                throw new InvalidEnumArgumentException("Invalid account type");
            } */

            return BankAccountDTO.ToDTO(await _accountRepository.AddAccount(BankAccountModel.CreationDTOToModel(account)));
        }

        public async Task<BankAccountDTO> DepositBalance(decimal deposit, int id, string clientCpf)
        {
            var account = await SearchAccountById(id);

            if (clientCpf != account.CPF)
            {
                throw new UnauthorizedAccessException("Invalid operation");
            }

            if (deposit <= 0)
            { 
                throw new ArgumentOutOfRangeException("Invalid deposit amount");
            }

            return BankAccountDTO.ToDTO(await _accountRepository.DepositBalance(deposit, id));
        }

        public async Task<BankAccountDTO> WithdrawBalance(decimal withdraw, int id, string clientCpf)
        {
            var account = await SearchAccountById(id);

            if (clientCpf != account.CPF)
            {
                throw new UnauthorizedAccessException("Invalid operation");
            }

            if (withdraw <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid withdraw amount");
            }

            if (account.Balance < withdraw)
            { 
                throw new InvalidOperationException("Insufficient balance");
            }
    
            return BankAccountDTO.ToDTO(await _accountRepository.WithdrawBalance(withdraw, id));
        }

        public async Task<BankAccountDTO> TransferBalance(decimal transfer, int accountId, int recipientId)
        {
            var accountById = await SearchAccountById(accountId);

            await SearchAccountById(recipientId);

            if (accountId == recipientId)
            {
                throw new ArgumentException("Source and destination account IDs must be different");
            }

            if (transfer <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid transfer balance");
            }

            if (accountById.Balance < transfer)
            {
                throw new InvalidOperationException("Insufficient balance");
            }
            
            return BankAccountDTO.ToDTO(await _accountRepository.TransferBalance(transfer, accountId, recipientId));
        }

        public async Task<bool> DeleteAccount(int id, string clientCpf)
        {
            var account = await SearchAccountById(id);

            if (clientCpf != account.CPF)
            {
                throw new UnauthorizedAccessException("Invalid operation");
            }

            return await _accountRepository.DeleteAccount(id);
        }
    }
}