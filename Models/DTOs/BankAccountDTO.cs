using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;
using BankAccountAPI.Models;

namespace BankAccountAPI.Models.DTOs
{
    public record BankAccountDTO
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public EnumAccountType AccountType { get; set; } 
        public decimal Balance { get; set; }
    

        public static BankAccountDTO ToDTO(BankAccountModel model)
        {
            return new BankAccountDTO
            {
                Id = model.AccountId,
                CPF = model.CPF,
                AccountType = model.AccountType,
                Balance = model.Balance
            };
        }
    }
}