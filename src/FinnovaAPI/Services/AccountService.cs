using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using FinnovaAPI.Models.DTOs.Account;
using FinnovaAPI.Repositories.Interfaces;
using FinnovaAPI.Helpers;

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
            AccountValidator.ValidateId(id);
            BankAccountModel accountById = await _accountRepository.SearchAccountById(id) ?? throw new KeyNotFoundException($"Account not found");

            return BankAccountDTO.ToDTO(accountById);
        }


        public async Task<BankAccountDTO> GetAuthenticatedClientAccount(int id, int clientId)
        {
            var account = await SearchAccountById(id);

            if (clientId != account.ClientId)
            {
                throw new UnauthorizedAccessException("You are not authorized to acess this account");
            }

            return account;
        }

        public async Task<List<AccountPreviewDTO>> SearchAllAccountsByClientId(int id)
        {
            var client = await _clientServices.SearchClientById(id);
            var accounts = await _accountRepository.SearchAllAccountsByClientId(id) ?? throw new KeyNotFoundException("Account not found with this client id");

            return accounts.Select(account => new AccountPreviewDTO
            {
                Id = account.Id,
                ClientId = account.ClientId,
                Name = client.Name,
                AccountType = account.AccountType
            }).ToList();
        }

        public async Task<BankAccountDTO> AddAccount(CreateAccountDTO account, int clientId)
        {
            // Validate Client id and ensure client exists
            await _clientServices.SearchClientById(clientId);

            account.ClientId = clientId;

            return BankAccountDTO.ToDTO(await _accountRepository.AddAccount(BankAccountModel.CreationDTOToModel(account)));
        }

        public async Task<BankAccountDTO> DepositBalance(DepositDTO deposit, int clientId)
        {
            var account = await GetAuthenticatedAccountModel(deposit.Id, clientId);
            account.Deposit(deposit.Amount);

            return BankAccountDTO.ToDTO(await _accountRepository.UpdateBalance(account));
        }

        public async Task<BankAccountDTO> WithdrawBalance(WithdrawDTO withdraw, int clientId)
        {
            var account = await GetAuthenticatedAccountModel(withdraw.Id, clientId);
            account.Withdraw(withdraw.Amount);

            return BankAccountDTO.ToDTO(await _accountRepository.UpdateBalance(account));
        }

        public async Task<BankAccountDTO> TransferBalance(TransferDTO transfer, int clientId)
        {
            var sourceAccount = await GetAuthenticatedAccountModel(transfer.SenderAccountId, clientId);

            var recipientAccount = await GetAccountModelById(transfer.RecipientId);

            if (sourceAccount.Id == recipientAccount.ClientId)
            {
                throw new ArgumentException("Source and destination account IDs must be different");
            }

            sourceAccount.Withdraw(transfer.Amount);
            recipientAccount.Deposit(transfer.Amount);

            await _accountRepository.UpdateBalance(sourceAccount);
            await _accountRepository.UpdateBalance(recipientAccount);

            return BankAccountDTO.ToDTO(sourceAccount);
        }

        public async Task<bool> DeleteAccount(int id, int clientId)
        {
            var account = await GetAuthenticatedAccountModel(id, clientId);

            return await _accountRepository.DeleteAccount(account);
        }

        private async Task<BankAccountModel> GetAccountModelById(int id)
        {
            return await _accountRepository.SearchAccountById(id) ?? throw new KeyNotFoundException("Account not found");
        }
        
        private async Task<BankAccountModel> GetAuthenticatedAccountModel(int id, int clientId)
        {
            var account = await _accountRepository.SearchAccountById(id) ?? throw new KeyNotFoundException("Account not found");

            if (clientId != account.ClientId)
            {
                throw new UnauthorizedAccessException("You are not authorized to acess this account");
            }

            return account;
        }
    }
}