using System.Net.Mail;
using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using FinnovaAPI.Models.DTOs.Client;
using FinnovaAPI.Repositories.Interfaces;
using FinnovaAPI.Helpers;
using FinnovaAPI.Models.DTOs;

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
            return BankClientDTO.ToDTO(await GetClientModelById(id));
        }

        public async Task<BankClientDTO> SearchClientByCPF(string cpf)
        {
            return await SearchClient(cpf, _clientRepository.SearchClientByCPF);
        }

        public async Task<BankClientDTO> SearchClientByEmail(string email)
        {
            return await SearchClient(email, _clientRepository.SearchClientByEmail);
        }

        public async Task<BankClientDTO> SearchClientByPhone(string phone)
        {
            return await SearchClient(phone, _clientRepository.SearchClientByPhone);
        }

        public async Task<ClientValidationRequestDTO> ValidateClientInfo(ClientValidationRequestDTO client)
        {
            await EnsureClientInfoIsUnique(client);
            await _verificationCodeService.SendAndSaveCode(client.Email);

            return client;
        }

        public async Task<BankClientDTO> AddClient(RegisterClientDTO registerClient)
        {
            await EnsureClientInfoIsUnique(registerClient);

            //check and delete if the code has expired
            await _verificationCodeService.ValidateCode(registerClient.Email, registerClient.Code);

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

            return BankClientDTO.ToDTO(await _clientRepository.UpdateClient(updatedClient));
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
            var client = await GetClientModelById(id);
            return await _clientRepository.DeleteClient(client);
        }

        private async Task<BankClientModel> GetClientModelById(int id)
        {
            ClientValidator.ValidateId(id);
            return await _clientRepository.SearchClientById(id) ?? throw new KeyNotFoundException("Client not found with this id");
        }

        private async Task EnsureClientInfoIsUnique(IClientInfo client)
        { 
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
        }

        private async Task<BankClientDTO> SearchClient(string value, Func<string, Task<BankClientModel>> searchFunc)
        {
            return BankClientDTO.ToDTO(await searchFunc(value)) ?? throw new KeyNotFoundException("Client not found");
        }

        private async Task<bool> ClientExists(string value, Func<string, Task<BankClientModel>> searchFunc)
        {
            return await searchFunc(value) != null;
        }
    }
}