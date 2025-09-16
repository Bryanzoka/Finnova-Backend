using FinnovaAPI.Models;
using FinnovaAPI.Models.DTOs.Client;

namespace FinnovaAPI.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<BankClientModel>> SearchAllClients();
        Task<BankClientDTO> SearchClientById(int id);
        Task<BankClientDTO> SearchClientByCPF(string cpf);
        Task<BankClientDTO> SearchClientByEmail(string email);
        Task<BankClientDTO> SearchClientByPhone(string phone);
        Task<ClientValidationRequestDTO> ValidateClientInfo(ClientValidationRequestDTO client);
        Task<BankClientDTO> AddClient(RegisterClientDTO client);
        Task<BankClientDTO> UpdateClient(UpdateClientDTO client, int id);
        Task<BankClientModel> ValidateCredentials(string cpf, string password);
        Task<bool> DeleteClient(int id);
    }
}