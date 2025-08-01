using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Services.Interfaces
{
    public interface IVerificationCodeService
    {
        Task<ClientVerificationCodeModel> GetCodeByEmail(string email);
        Task SendAndSaveCode(ClientVerificationCodeModel code);
        Task<bool> DeleteCode(string email);
    }
}