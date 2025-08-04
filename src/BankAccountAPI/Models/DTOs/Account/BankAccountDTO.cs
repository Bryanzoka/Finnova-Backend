using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;
using BankAccountAPI.Models;

namespace BankAccountAPI.Models.DTOs.Account
{
    public record BankAccountDTO
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public EnumAccountType AccountType { get; set; } 
        public decimal Balance { get; set; }


        public static BankAccountDTO ToDTO(BankAccountModel model)
        {
            return new BankAccountDTO
            {
                Id = model.Id,
                Cpf = model.Cpf,
                AccountType = model.AccountType,
                Balance = model.Balance
            };
        }
    }
}