using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Data;
using BankAccountAPI.Models;
using BankAccountAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly BankAccountDBContext _dbContext;

        public VerificationCodeRepository(BankAccountDBContext bankAccountDBContext)
        {
            _dbContext = bankAccountDBContext;
        }

        public async Task<ClientVerificationCodeModel> GetCodeByEmail(string email)
        {
            ClientVerificationCodeModel code = await _dbContext.ClientVerificationCode.FirstOrDefaultAsync(c => c.Email == email);

            return code;
        }

        public async Task SaveCode(ClientVerificationCodeModel code)
        {
            await _dbContext.AddAsync(code);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteCode(string email)
        {
            ClientVerificationCodeModel code = await GetCodeByEmail(email);

            _dbContext.ClientVerificationCode.Remove(code);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}