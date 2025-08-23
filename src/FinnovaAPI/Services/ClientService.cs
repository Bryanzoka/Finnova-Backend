using System.Net.Mail;
using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using FinnovaAPI.Models.DTOs.Client;
using FinnovaAPI.Repositories.Interfaces;
using FinnovaAPI.Helpers;

namespace FinnovaAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IVerificationCodeService _verificationCodeService;

        public ClientService(IClientRepository clientRepository, IPasswordHasherService passwordHasher, IVerificationCodeService verificationCodeService)
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
            _verificationCodeService = verificationCodeService;
        }

        public async Task<List<BankClientModel>> SearchAllClients()
        {
            return await _clientRepository.SearchAllClients();
        }

        public async Task<BankClientDTO> SearchClientById(int id)
        {
            ClientValidator.ValidateId(id);
            var client = await _clientRepository.SearchClientById(id) ?? throw new KeyNotFoundException("Client not found");
            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientDTO> SearchClientByCPF(string cpf)
        {
            return await ValidateAndSearchClient(cpf, ClientValidator.ValidateCpf, _clientRepository.SearchClientByCPF);
        }

        public async Task<BankClientDTO> SearchClientByEmail(string email)
        {
            return await ValidateAndSearchClient(email, ClientValidator.ValidateEmail, _clientRepository.SearchClientByEmail);
        }

        public async Task<BankClientDTO> SearchClientByPhone(string phone)
        {
            return await ValidateAndSearchClient(phone, ClientValidator.ValidateEmail, _clientRepository.SearchClientByPhone);
        }

        public async Task<ClientValidationRequestDTO> ValidateClientInfo(ClientValidationRequestDTO client)
        {
            ClientValidator.ValidateCpf(client.Cpf);
            ClientValidator.ValidateEmail(client.Email);
            ClientValidator.ValidatePhone(client.Phone);

            if (await ClientExists(client.Cpf, _clientRepository.SearchClientByCPF))
            {
                throw new InvalidOperationException("Client with this CPF already exists");
            }

            if (await ClientExists(client.Email, _clientRepository.SearchClientByEmail))
            {
                throw new InvalidOperationException("Client with this email already exists");
            }

            if (await ClientExists(client.Phone, _clientRepository.SearchClientByPhone))
            {
                throw new InvalidOperationException("Client with this phone already exists");
            }

            string code = GenerateRandomCode();

            var verificationCode = new ClientVerificationCodeModel(client.Email, code);

            await _verificationCodeService.SendAndSaveCode(verificationCode);

            return client;
        }

        public async Task<BankClientDTO> AddClient(RegisterClientDTO registerClient)
        {
            ClientValidator.ValidateCpf(registerClient.Cpf);
            ClientValidator.ValidateEmail(registerClient.Email);
            ClientValidator.ValidatePhone(registerClient.Phone);

            if (await ClientExists(registerClient.Cpf, _clientRepository.SearchClientByCPF))
            {
                throw new InvalidOperationException("Client with this CPF already exists");
            }

            if (await ClientExists(registerClient.Email, _clientRepository.SearchClientByEmail))
            {
                throw new InvalidOperationException("Client with this email already exists");
            }

            if (await ClientExists(registerClient.Phone, _clientRepository.SearchClientByPhone))
            {
                throw new InvalidOperationException("Client with this phone already exists");
            }

            var verificationCode = await _verificationCodeService.GetCodeByEmail(registerClient.Email);

            //check and delete if the code has expired
            if (DateTime.UtcNow > verificationCode.Expiration)
            {
                await _verificationCodeService.DeleteCode(registerClient.Email);
                throw new InvalidOperationException("Verification code has expired");
            }

            if (registerClient.Code != verificationCode.Code)
            {
                throw new UnauthorizedAccessException("Invalid verification code");
            }

            var client = BankClientModel.FromRegister(registerClient);

            client.HashPassword(_passwordHasher.HashPassword(client.Password));
            await _clientRepository.AddClient(client);

            //Delete code after registering client
            await _verificationCodeService.DeleteCode(registerClient.Email);

            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientDTO> UpdateClient(UpdateClientDTO client, int id)
        {
            var updatedClient = await GetClientModelById(id);

            if (client.Password != null)
            {
                client.Password = _passwordHasher.HashPassword(client.Password);
            }

            updatedClient.UpdateClient(
                client.Name ?? updatedClient.Name,
                client.Email ?? updatedClient.Email,
                client.Phone ?? updatedClient.Phone,
                client.Password ?? updatedClient.Password
            );

            return BankClientDTO.ToDTO(await _clientRepository.UpdateClient(updatedClient, id));
        }

        public async Task<BankClientModel> ValidateCredentials(string cpf, string password)
        {
            var client = await _clientRepository.SearchClientByCPF(cpf) ?? throw new KeyNotFoundException("Account with informed CPF does not exist");

            if (!_passwordHasher.VerifyPassword(password, client.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return client;
        }

        public async Task<bool> DeleteClient(int id)
        {
            await GetClientModelById(id);
            return await _clientRepository.DeleteClient(id);
        }

        private async Task<BankClientModel> GetClientModelById(int id)
        {
            ClientValidator.ValidateId(id);
            return await _clientRepository.SearchClientById(id) ?? throw new KeyNotFoundException("Client not found with this Id");
        }

        private static async Task<BankClientDTO> ValidateAndSearchClient(string value, Action<string> validate, Func<string, Task<BankClientModel>> searchFunc)
        {
            validate(value);
            return BankClientDTO.ToDTO(await searchFunc(value));
        }

        private static async Task<bool> ClientExists(string value, Func<string, Task<BankClientModel>> searchFunc)
        {
            return await searchFunc(value) != null;
        }

        private string GenerateRandomCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}