using FinnovaAPI.Data;
using FinnovaAPI.Models;
using FinnovaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinnovaAPI.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public VerificationCodeRepository(FinnovaDbContext finnovaDbContext)
        {
            _dbContext = finnovaDbContext;
        }

        public async Task<ClientVerificationCodeModel> GetCodeByEmail(string email)
        {
            return await _dbContext.ClientVerificationCode.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task SaveCode(ClientVerificationCodeModel code)
        {
            await _dbContext.AddAsync(code);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteCode(ClientVerificationCodeModel code)
        {
            _dbContext.ClientVerificationCode.Remove(code);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}