using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;

namespace BankAccountAPI.Models.DTOs.Account
{
    public class AccountPreviewDTO
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public EnumAccountType AccountType { get; set; }
    }
}