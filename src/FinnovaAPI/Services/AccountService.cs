using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using FinnovaAPI.Models.DTOs.Account;
using FinnovaAPI.Repositories.Interfaces;

namespace FinnovaAPI.Services
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

        public async Task<List<AccountPreviewDTO>> SearchAllAccountsByCpf(string cpf)
        {
            var client = await _clientServices.SearchClientByCPF(cpf);
            var accounts = await _accountRepository.SearchAllAccountsByCpf(cpf) ?? throw new KeyNotFoundException("Account not found with this CPF");

            return accounts.Select(account => new AccountPreviewDTO
            {
                Id = account.Id,
                Cpf = account.Cpf,
                Name = client.Name,
                AccountType = account.AccountType
            }).ToList();
        }

        public async Task<BankAccountDTO> AddAccount(CreateAccountDTO account)
        {
            // Validate CPF and ensure client exists
            await _clientServices.SearchClientByCPF(account.Cpf);

            return BankAccountDTO.ToDTO(await _accountRepository.AddAccount(BankAccountModel.CreationDTOToModel(account)));
        }

        public async Task<BankAccountDTO> DepositBalance(DepositDTO deposit, string clientCpf)
        {
            var account = await SearchAccountById(deposit.Id);

            if (clientCpf != account.Cpf)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this account");
            }

            if (deposit.Amount <= 0)
            { 
                throw new ArgumentOutOfRangeException("Deposit amount must be greater than 0");
            }

            return BankAccountDTO.ToDTO(await _accountRepository.DepositBalance(deposit.Amount, deposit.Id));
        }

        public async Task<BankAccountDTO> WithdrawBalance(WithdrawDTO withdraw, string clientCpf)
        {
            var account = await SearchAccountById(withdraw.Id);

            if (clientCpf != account.Cpf)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this account");
            }

            if (withdraw.Amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Withdraw amount must be greater than 0");
            }

            if (account.Balance < withdraw.Amount)
            { 
                throw new InvalidOperationException("Insufficient balance");
            }
    
            return BankAccountDTO.ToDTO(await _accountRepository.WithdrawBalance(withdraw.Amount, withdraw.Id));
        }

        public async Task<BankAccountDTO> TransferBalance(decimal transfer, int accountId, int recipientId, string clientCpf)
        {
            var accountById = await SearchAccountById(accountId);

            if (accountById.Cpf != clientCpf)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this account");
            }

            await SearchAccountById(recipientId);

            if (accountId == recipientId)
            {
                throw new ArgumentException("Source and destination account IDs must be different");
            }

            if (transfer <= 0)
            {
                throw new ArgumentOutOfRangeException("Transfer amount must be greater than 0");
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

            if (clientCpf != account.Cpf)
            {
                throw new UnauthorizedAccessException("Invalid operation");
            }

            return await _accountRepository.DeleteAccount(id);
        }
    }
}