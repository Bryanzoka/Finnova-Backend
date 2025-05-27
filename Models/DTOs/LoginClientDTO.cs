using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models.DTOs
{
    public record LoginClientDTO
    {
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}