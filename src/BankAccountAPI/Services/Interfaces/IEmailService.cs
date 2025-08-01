using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendVerificationCode(string toEmail, string verificationCode);
    }
}