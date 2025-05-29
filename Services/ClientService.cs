using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
using BankAccountAPI.Repository;
using BankAccountAPI.Models.DTOs;

namespace BankAccountAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<BankClientModel>> SearchAllClients()
        {
            return await _clientRepository.SearchAllClients();
        }

        public async Task<BankClientDTO> SearchClientByCPF(string cpf)
        {
            if(cpf.Length != 11 || !cpf.All(char.IsDigit))
                return null;

            var client = await _clientRepository.SearchClientByCPF(cpf);
            return BankClientDTO.ToDTO(client);
        }

        public async Task<BankClientModel> AddClient(BankClientModel client)
        {
            return await _clientRepository.AddClient(client);
        }

        public async Task<BankClientDTO> UpdateClient(UpdateClientDTO client, string cpf)
        {
            if (cpf.Length != 11 || !cpf.All(char.IsDigit)) throw new ArgumentException("CPF inválido");
            var updatedClient = await _clientRepository.SearchClientByCPF(cpf) ?? throw new KeyNotFoundException("Conta com o CPF informado não encontrado");
            updatedClient.UpdateClient(
                client.ClientName ?? updatedClient.ClientName,
                client.ClientEmail ?? updatedClient.ClientEmail,
                client.ClientTel ?? updatedClient.ClientTel,
                client.Password ?? updatedClient.Password,
                DateTime.Now
            );

            await _clientRepository.UpdateClient(updatedClient, cpf);
            return BankClientDTO.ToDTO(updatedClient);
        }

        public async Task<BankClientModel> ValidateCredentials(string cpf, string password)
        {
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                throw new ArgumentException("CPF inválido");

            var client = await _clientRepository.SearchClientByCPF(cpf);
            if (client == null || client.Password != password)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            return client;
        }

        public async Task<bool> DeleteClient(string cpf)
        {
            if (await SearchClientByCPF(cpf) == null) throw new ArgumentException("Conta não encontrada");
            return await _clientRepository.DeleteClient(cpf);
        }
    }
}