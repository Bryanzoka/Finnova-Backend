using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services.Interface
{
    public interface IEmailService
    {
        Task SendVerificationCode(string toEmail, string validationCode);
    }
}