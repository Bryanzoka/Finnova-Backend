using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Repository
{
    public interface IClientRepository
    {
        Task<List<BankClientModel>> SearchAllClients();
        Task<BankClientModel> SearchClientByCPF(string cpf);
        Task<BankClientModel> SearchClientByEmail(string email);
        Task<BankClientModel> SearchClientByPhone(string phone);
        Task<BankClientModel> AddClient(BankClientModel client);  
        Task<BankClientModel> UpdateClient(BankClientModel client, string cpf);
        Task<bool> DeleteClient(string cpf);
    }
}