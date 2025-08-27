using FinnovaAPI.Models;

namespace FinnovaAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<List<BankClientModel>> SearchAllClients();
        Task<BankClientModel> SearchClientById(int id);
        Task<BankClientModel> SearchClientByCPF(string cpf);
        Task<BankClientModel> SearchClientByEmail(string email);
        Task<BankClientModel> SearchClientByPhone(string phone);
        Task<BankClientModel> AddClient(BankClientModel client);  
        Task<BankClientModel> UpdateClient(BankClientModel client);
        Task<bool> DeleteClient(BankClientModel client);
    }
}