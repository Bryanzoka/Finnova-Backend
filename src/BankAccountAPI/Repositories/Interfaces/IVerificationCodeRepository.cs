using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Repositories.Interfaces
{
    public interface IVerificationCodeRepository
    {
        Task<ClientVerificationCodeModel> GetCodeByEmail(string email);
        Task SaveCode(ClientVerificationCodeModel code);
        Task<bool> DeleteCode(string email);
    }
}