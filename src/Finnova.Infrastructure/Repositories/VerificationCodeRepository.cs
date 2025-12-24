using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using Finnova.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly FinnovaDbContext _dbContext;

        public VerificationCodeRepository(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VerificationCode?> GetByEmailAsync(string email)
        {
            return await _dbContext.VerificationCodes.FirstOrDefaultAsync(v => v.Email == email);
        }

        public async Task AddAsync(VerificationCode verificationCode)
        {
            await _dbContext.VerificationCodes.AddAsync(verificationCode);
        }

        public void Delete(VerificationCode verificationCode)
        {
            _dbContext.VerificationCodes.Remove(verificationCode);
        }
    }
}