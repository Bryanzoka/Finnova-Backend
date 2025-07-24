using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
using BankAccountAPI.Repository;
using BankAccountAPI.Models.DTOs;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionTranslators.Internal;

namespace BankAccountAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IEmailService _emailService;

        public ClientService(IClientRepository clientRepository, IPasswordHasherService passwordHasher, IEmailService emailService)
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<List<BankClientModel>> SearchAllClients()
        {
            return await _clientRepository.SearchAllClients();
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
            if (await ClientExistByCPF(client.CPF))
            {
                throw new InvalidOperationException("Client with this CPF already exists");
            }

            if (await ClientExistByEmail(client.Email))
            {
                throw new InvalidOperationException("Client with this email already exists");
            }

            if (await ClientExistByPhone(client.Phone))
            {
                throw new InvalidOperationException("Client with this phone already exists");
            }

            string code = GenerateRandomCode();

            await _emailService.SendVerificationCode(client.Email, code);

            return client;
        }

        public async Task<BankClientModel> AddClient(BankClientModel client)
        {
            if (await ClientExistByCPF(client.CPF))
            {
                throw new InvalidOperationException("Client with this CPF already exists");
            }

            client.HashPassword(_passwordHasher.HashPassword(client.Password));
            return await _clientRepository.AddClient(client);
        }

        public async Task<BankClientDTO> UpdateClient(UpdateClientDTO client, string cpf)
        {
            var updatedClient = BankClientModel.ToModel(await SearchClientByCPF(cpf));

            if (client.Password != null)
            {
                client.Password = _passwordHasher.HashPassword(client.Password);
            }

            updatedClient.UpdateClient(
                client.ClientName ?? updatedClient.ClientName,
                client.ClientEmail ?? updatedClient.ClientEmail,
                client.ClientTel ?? updatedClient.ClientTel,
                client.Password ?? updatedClient.Password
            );

            await _clientRepository.UpdateClient(updatedClient, cpf);
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

        public async Task<bool> DeleteClient(string cpf)
        {
            await SearchClientByCPF(cpf);

            return await _clientRepository.DeleteClient(cpf);
        }

        private async Task<bool> ClientExistByCPF(string cpf)
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

        private async Task<bool> ClientExistByEmail(string email)
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

        private async Task<bool> ClientExistByPhone(string phone)
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