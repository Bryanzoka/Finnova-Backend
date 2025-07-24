using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using BankAccountAPI.Models.DTOs;

namespace BankAccountAPI.Services.Interface
{
    public interface IClientService
    {
        Task<List<BankClientModel>> SearchAllClients();
        Task<BankClientDTO> SearchClientByCPF(string cpf);
        Task<BankClientDTO> SearchClientByEmail(string email);
        Task<BankClientDTO> SearchClientByPhone(string phone);
        Task<ClientValidationRequestDTO> ValidateClientInfo(ClientValidationRequestDTO client);  
        Task<BankClientDTO> UpdateClient(UpdateClientDTO client, string cpf);
        Task<BankClientModel> ValidateCredentials(string cpf, string password);
        Task<bool> DeleteClient(string cpf);
    }
}