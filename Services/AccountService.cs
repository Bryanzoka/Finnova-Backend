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
                throw new ArgumentException($"O ID: {id} é inválido");
            }

            BankAccountModel accountById = await _accountRepository.SearchAccountById(id) ?? throw new KeyNotFoundException($"Conta não encontrada");

            return BankAccountDTO.ToDTO(accountById);
        }

        public async Task<BankAccountModel> AddAccount(CreateAccountDTO account)
        {
            if (string.IsNullOrEmpty(account.CPF) || !account.CPF.All(char.IsDigit))
            {
                throw new ArgumentException("CPF inválido");
            }

            if (await _clientServices.SearchClientByCPF(account.CPF) == null)
            {
                throw new KeyNotFoundException("Cliente com CPF informado não encontrado");
            }

            if (account.AccountType != EnumAccountType.Current && account.AccountType != EnumAccountType.Savings)
            { 
                throw new InvalidEnumArgumentException("Tipo de conta inválido");
            }

            return await _accountRepository.AddAccount(BankAccountModel.CreationDTOToModel(account));
        }

        public async Task<BankAccountDTO> DepositBalance(decimal deposit, int id)
        {
            if (await SearchAccountById(id) == null)
            {
                throw new KeyNotFoundException("Conta não encontrada");
            }

            if (deposit <= 0)
            { 
                throw new ArgumentOutOfRangeException("Valor de depósito inválido");
            }

            var account = await _accountRepository.DepositBalance(deposit, id);
            return BankAccountDTO.ToDTO(account);
        }

        public async Task<BankAccountModel> WithdrawBalance(decimal withdraw, int id)
        {
            var account = await SearchAccountById(id) ?? throw new Exception("Conta não encontrada");

            if (withdraw <= 0) throw new InvalidOperationException("Valor de saque inválido");
            if (account.Balance < withdraw) throw new InvalidOperationException("Saldo insuficiente");
    
            return await _accountRepository.WithdrawBalance(withdraw, id);
        }

        public async Task<BankAccountModel> TransferBalance(decimal transfer, int accountId, int recipientId)
        {
            var accountById = await SearchAccountById(accountId) ?? throw new Exception($"Conta de ID: {accountId} não encontrada");
            if(await SearchAccountById(recipientId) == null) throw new KeyNotFoundException($"Conta de ID: {recipientId} não encontrada");
            
            if(accountId == recipientId) throw new Exception("IDs idênticos são inválidos");
            if(transfer <= 0) throw new Exception("Saldo de transferência inválida");
            if (accountById.Balance < transfer) throw new Exception("Saldo insuficiente");
            
            return await _accountRepository.TransferBalance(transfer, accountId, recipientId);
        }

        public async Task<bool> DeleteAccount(int id)
        {
            if(await SearchAccountById(id) == null) throw new Exception("Conta não encontrada");

            return await _accountRepository.DeleteAccount(id);
        }
    }
}