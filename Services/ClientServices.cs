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
    public class ClientServices : IClientServices
    {
        private readonly IClientRepository _clientRepository;

        public ClientServices(IClientRepository clientRepository)
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
            ArgumentNullException.ThrowIfNull(client);
            if(string.IsNullOrEmpty(client.ClientName)) throw new ArgumentNullException("Nome inválido");
            if(client.CPF.Length != 11 || !client.CPF.All(char.IsDigit)) throw new ArgumentException("CPF inválido");
            MailAddress invalidEmail = new MailAddress(client.ClientEmail) ?? throw new FormatException("Email inválido");
            if(client.ClientTel.Length < 11 || client.ClientTel.Length > 13 || !client.ClientTel.All(char.IsDigit)) throw new FormatException("Telefone inválido");
            return await _clientRepository.AddClient(client);
        }

        public async Task<BankClientModel> UpdateClient(BankClientModel client, string cpf)
        {
            if(await SearchClientByCPF(cpf) == null) throw new ArgumentException("Conta não encontrada");
            ArgumentNullException.ThrowIfNull(client);
            if(string.IsNullOrEmpty(client.ClientName)) throw new ArgumentException("Nome inválido");
            MailAddress invalidEmail = new MailAddress(client.ClientEmail) ?? throw new FormatException("Email inválido");
            if(client.ClientTel.Length < 11 || client.ClientTel.Length > 13 || !client.ClientTel.All(char.IsDigit)) throw new FormatException("Telefone inválido");
            return await _clientRepository.UpdateClient(client, cpf);
        }

        public async Task<bool> DeleteClient(string cpf)
        {
            if(await SearchClientByCPF(cpf) == null) throw new ArgumentException("Conta não encontrada");
            return await _clientRepository.DeleteClient(cpf);
        }
    }
}