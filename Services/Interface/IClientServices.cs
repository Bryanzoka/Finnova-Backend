using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Services.Interface
{
    public interface IClientServices
    {
        Task<List<BankClientModel>> SearchAllClients();
        Task<BankClientModel> SearchClientByCPF(string cpf);
        Task<BankClientModel> AddClient(BankClientModel client);  
        Task<BankClientModel> UpdateClient(BankClientModel client, string cpf);
        Task<bool> DeleteClient(string cpf);
    }
}