using System.Net.Mail;
using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using FinnovaAPI.Models.DTOs.Client;
using FinnovaAPI.Repositories.Interfaces;

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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id number");
            }

            var client = await _clientRepository.SearchClientById(id) ?? throw new KeyNotFoundException("Client not found");
            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientDTO> SearchClientByCPF(string cpf)
        {
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid CPF");
            }

            var client = await _clientRepository.SearchClientByCPF(cpf) ?? throw new KeyNotFoundException("Client not found");
            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientDTO> SearchClientByEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("Invalid email format");
            }
            
            var client = await _clientRepository.SearchClientByEmail(email) ?? throw new KeyNotFoundException("Client not found");
            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientDTO> SearchClientByPhone(string phone)
        {
            if (phone.Length < 11 || phone.Length > 13 || !phone.All(char.IsDigit))
            {
                throw new ArgumentException("Phone number must contain only 11 to 13 digits");
            }

            var client = await _clientRepository.SearchClientByPhone(phone) ?? throw new KeyNotFoundException("Client not found");
            return BankClientDTO.ToDTO(client);
        }

        public async Task<ClientValidationRequestDTO> ValidateClientInfo(ClientValidationRequestDTO client)
        {
            if (await ClientExistsByCPF(client.Cpf))
            {
                throw new InvalidOperationException("Client with this CPF already exists");
            }

            if (await ClientExistsByEmail(client.Email))
            {
                throw new InvalidOperationException("Client with this email already exists");
            }

            if (await ClientExistsByPhone(client.Phone))
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
            if (await ClientExistsByCPF(registerClient.Cpf))
            {
                throw new InvalidOperationException("Client with this CPF already exists");
            }

            if (await ClientExistsByEmail(registerClient.Email))
            {
                throw new InvalidOperationException("Client with this email already exists");
            }

            if (await ClientExistsByPhone(registerClient.Phone))
            {
                throw new InvalidOperationException("Client with this phone already exists");
            }

            var verificationCode = await _verificationCodeService.GetCodeByEmail(registerClient.Email);

            if (registerClient.Code != verificationCode.Code)
            {
                throw new UnauthorizedAccessException("Invalid verification code");
            }

            var client = BankClientModel.FromRegister(registerClient);

            client.HashPassword(_passwordHasher.HashPassword(client.Password));
            await _clientRepository.AddClient(client);

            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientDTO> UpdateClient(UpdateClientDTO client, int id)
        {
            var updatedClient = BankClientModel.ToModel(await SearchClientById(id));

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

            await _clientRepository.UpdateClient(updatedClient, id);
            return BankClientDTO.ToDTO(updatedClient);
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
            await SearchClientById(id);

            return await _clientRepository.DeleteClient(id);
        }

        private async Task<bool> ClientExistsByCPF(string cpf)
        {
            try
            {
                await SearchClientByCPF(cpf);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private async Task<bool> ClientExistsByEmail(string email)
        {
            try
            {
                await SearchClientByEmail(email);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private async Task<bool> ClientExistsByPhone(string phone)
        {
            try
            {
                await SearchClientByPhone(phone);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private string GenerateRandomCode()
        {
            var random = new Random();
            string code = random.Next(100000, 999999).ToString();
            return code;
        }
    }
}