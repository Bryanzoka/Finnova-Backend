using Finnova.Domain.Aggregates;

namespace FinnovaAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByCpfAsync(string cpf);
        Task<bool> CpfExistsAsync(string cpf);
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(Client client);  
        void Update(Client client);
    }
}