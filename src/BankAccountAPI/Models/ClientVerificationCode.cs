using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models
{
    public class ClientVerificationCodeModel
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Code { get; private set; }
        public DateTime Expiration { get; private set; }

        public ClientVerificationCodeModel(string email, string code)
        {
            Email = email;
            Code = code;
            Expiration = DateTime.UtcNow.AddMinutes(10);
        }
    }
}