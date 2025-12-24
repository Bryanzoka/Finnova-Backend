using Finnova.Domain.Entities;
using Finnova.Infrastructure.Persistence;
using FinnovaAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public UserRepository(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync(); 
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _dbContext.Update(user);
        }

        public void Delete(User user)
        {
            _dbContext.Remove(user);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(c => c.Email == email);
        }
    }
}