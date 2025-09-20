using Finnova.Domain.Aggregates;

namespace FinnovaAPI.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetByCpfAsync(string cpf);
        Task<bool> CpfExistsAsync(string cpf);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneExistsAsync(string phone);
        Task AddAsync(Client client);  
        void Update(Client client);
    }
}