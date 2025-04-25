using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;
using BankAccountAPI.Models;

namespace BankAccountAPI.Models.DTOs
{
    public class CreateAccountDTO
    {
        public string CPF { get; set; }
        public EnumAccountType AccountType { get; set; }
    }
}