using Finnova.Domain.Aggregates;
using Finnova.Infrastructure.Persistence;
using FinnovaAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public ClientRepository(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _dbContext.Clients.ToListAsync(); 
        }

        public async Task<Client?> GetByCpfAsync(string cpf)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
        }

        public void Update(Client client)
        {
            _dbContext.Update(client);
        }

        public void Delete(Client client)
        {
            _dbContext.Remove(client);
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _dbContext.Clients.AsNoTracking().AnyAsync(c => c.Cpf == cpf);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Clients.AsNoTracking().AnyAsync(c => c.Email == email);
        }

        public async Task<bool> PhoneExistsAsync(string phone)
        {
            return await _dbContext.Clients.AsNoTracking().AnyAsync(c => c.Phone == phone);
        }
    }
}