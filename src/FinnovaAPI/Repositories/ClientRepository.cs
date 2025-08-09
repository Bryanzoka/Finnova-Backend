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

        public async Task<BankClientModel> SearchClientByCPF(string cpf)
        {
            BankClientModel clientByCPF = await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Cpf == cpf);

            return clientByCPF;
        }
        
        public async Task<BankClientModel> SearchClientByEmail(string email)
        {
            BankClientModel clientByEmail = await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Email == email);

            return clientByEmail;
        }

        public async Task<BankClientModel> SearchClientByPhone(string phone)
        {
            BankClientModel clientByPhone = await _dbContext.BankClient.FirstOrDefaultAsync(c => c.Phone == phone);

            return clientByPhone;
        }

        public async Task<BankClientModel> AddClient(BankClientModel client)
        {
            await _dbContext.BankClient.AddAsync(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<BankClientModel> UpdateClient(BankClientModel client, string cpf)
        {
            BankClientModel clientByCPF = await SearchClientByCPF(cpf);

            clientByCPF.UpdateClient(client.Name, client.Email, client.Phone, client.Password);

            _dbContext.BankClient.Update(clientByCPF);
            await _dbContext.SaveChangesAsync();

            return clientByCPF;
        }

        public async Task<bool> DeleteClient(string cpf)
        {
            BankClientModel clientByCPF = await SearchClientByCPF(cpf);

            _dbContext.BankClient.Remove(clientByCPF);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}