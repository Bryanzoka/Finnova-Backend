using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
using BankAccountAPI.Repository;

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

        public async Task<BankClientModel> SearchClientByCPF(string cpf)
        {
            if(cpf.Length != 11 || !cpf.All(char.IsDigit)) throw new ArgumentException("CPF inválido");
            return await _clientRepository.SearchClientByCPF(cpf);
        }

        public async Task<BankClientModel> AddClient(BankClientModel client)
        {
            return await _clientRepository.AddClient(client);
        }

        public async Task<BankClientModel> UpdateClient(BankClientModel client, string cpf)
        {
            return await _clientRepository.UpdateClient(client, cpf);
        }

        public async Task<BankClientModel> ValidateCredentials(string cpf, string password)
        {
            BankClientModel client = await SearchClientByCPF(cpf);
            if (client == null || client.Password != password) return null;
            return client;
        }

        public async Task<bool> DeleteClient(string cpf)
        {
            if (await SearchClientByCPF(cpf) == null) throw new ArgumentException("Conta não encontrada");
            return await _clientRepository.DeleteClient(cpf);
        }
    }
}