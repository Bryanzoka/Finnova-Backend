using FinnovaAPI.Models;

namespace FinnovaAPI.Repositories.Interfaces
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