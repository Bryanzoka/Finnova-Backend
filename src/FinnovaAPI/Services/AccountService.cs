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
        private readonly IPasswordHasherService _passwordHasher;

        public AccountService(IAccountRepository accountRepository, IClientService clientServices, IPasswordHasherService passwordHasher)
        {
            _accountRepository = accountRepository;
            _clientServices = clientServices;
            _passwordHasher = passwordHasher;
        }
        public async Task<List<BankAccountModel>> SearchAllAccounts()
        {
            return await _accountRepository.SearchAllAccounts();
        }

        public async Task<BankAccountDTO> SearchAccountById(int id)
        {
            return BankAccountDTO.ToDTO(await GetAccountModelById(id));
        }


        public async Task<BankAccountDTO> GetAuthenticatedClientAccount(int id, int clientId)
        {
            return BankAccountDTO.ToDTO(await GetAuthenticatedAccountModel(id, clientId));
        }

        public async Task<List<AccountPreviewDTO>> SearchAllAccountsByClientId(int id)
        {
            var client = await _clientServices.SearchClientById(id);
            var accounts = await _accountRepository.SearchAllAccountsByClientId(id) ?? throw new KeyNotFoundException("No accounts found for this client");

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

            account.Password = _passwordHasher.HashPassword(account.Password);

            return BankAccountDTO.ToDTO(await _accountRepository.AddAccount(BankAccountModel.CreationDTOToModel(account)));
        }

        public async Task<BankAccountDTO> UpdateAccount(UpdateAccountDTO account, int clientId)
        {
            var bankAccount = await GetAuthenticatedAccountModel(account.Id, clientId);
            VerifyPassword(account.Password, bankAccount.Password);

            if (account.Password != null)
            {
                bankAccount.UpdateAccount(_passwordHasher.HashPassword(account.NewPassword));
            }

            return BankAccountDTO.ToDTO(await _accountRepository.UpdateAccount(bankAccount));
            //$2a$11$2K.kOV78qDVI1vNK/d0t/On1gWZAYN7x/fB6YOgBVz6yHu1Rl2DMW
        }

        public async Task<BankAccountDTO> DepositBalance(DepositDTO deposit, int clientId)
        {
            var account = await GetAuthenticatedAccountModel(deposit.Id, clientId);
            VerifyPassword(deposit.Password, account.Password);

            account.Deposit(deposit.Amount);

            return BankAccountDTO.ToDTO(await _accountRepository.UpdateAccount(account));
        }

        public async Task<BankAccountDTO> WithdrawBalance(WithdrawDTO withdraw, int clientId)
        {
            var account = await GetAuthenticatedAccountModel(withdraw.Id, clientId);
            VerifyPassword(withdraw.Password, account.Password);

            account.Withdraw(withdraw.Amount);

            return BankAccountDTO.ToDTO(await _accountRepository.UpdateAccount(account));
        }

        public async Task<BankAccountDTO> TransferBalance(TransferDTO transfer, int clientId)
        {
            var sourceAccount = await GetAuthenticatedAccountModel(transfer.SenderAccountId, clientId);

            VerifyPassword(transfer.Password, sourceAccount.Password);

            var recipientAccount = await GetAccountModelById(transfer.RecipientId);

            if (sourceAccount.Id == recipientAccount.Id)
            {
                throw new ArgumentException("Source and destination account IDs must be different");
            }

            sourceAccount.Withdraw(transfer.Amount);
            recipientAccount.Deposit(transfer.Amount);

            await _accountRepository.UpdateAccount(sourceAccount);
            await _accountRepository.UpdateAccount(recipientAccount);

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

        private void VerifyPassword(string password, string hashedPassword)
        { 
            if (!_passwordHasher.VerifyPassword(password, hashedPassword))
            {
                throw new UnauthorizedAccessException("Incorrect password");
            }
        }
    }
}