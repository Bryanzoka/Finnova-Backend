using FinnovaAPI.Models;
using FinnovaAPI.Data;
using Microsoft.EntityFrameworkCore;
using FinnovaAPI.Repositories.Interfaces;

namespace FinnovaAPI.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly FinnovaDbContext _dbContext;
        public ClientRepository(FinnovaDbContext finnovaDbContext)
        {
            _dbContext = finnovaDbContext;
        }

        public async Task<List<BankClientModel>> SearchAllClients()
        {
           return await _dbContext.BankClient.ToListAsync(); 
        }

        public async Task<BankClientModel> SearchClientById(int id)
        {
            return await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<BankClientModel> SearchClientByCPF(string cpf)
        {
            return await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }
        
        public async Task<BankClientModel> SearchClientByEmail(string email)
        {
            return await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<BankClientModel> SearchClientByPhone(string phone)
        {
            return await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<BankClientModel> AddClient(BankClientModel client)
        {
            await _dbContext.BankClient.AddAsync(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<BankClientModel> UpdateClient(BankClientModel client)
        {
            _dbContext.BankClient.Update(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<bool> DeleteClient(BankClientModel client)
        {
            _dbContext.BankClient.Remove(client);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}