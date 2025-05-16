using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using BankAccountAPI.Models.DTOs;
using BankAccountAPI.Controllers;
using Microsoft.AspNetCore.Identity;

namespace BankAccountAPI.Models
{
    public class BankAccountModel
    {
        [Key]
        public int AccountId { get; private set; }

        [Required]
        [MaxLength(11)]
        [ForeignKey("BankClient")]
        public string CPF { get; private set; }
        public EnumAccountType AccountType { get; private set; } 
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastTransactionAt { get; private set; }
        public virtual BankClientModel BankClient { get; private set; } 

        public BankAccountModel() {}

        [JsonConstructor]
        public BankAccountModel(int id, string cpf, EnumAccountType accounttype)
        {
            AccountId = id;
            CPF = cpf;
            AccountType = accounttype;
            Balance = 0;
        }

        public static BankAccountModel ToModel(BankAccountDTO dto)
        {
            return new BankAccountModel
            {
                AccountId = dto.Id,
                CPF = dto.CPF,
                AccountType = dto.AccountType,
                Balance = dto.Balance
            };
        }

        public static BankAccountModel CreationDTOToModel(CreateAccountDTO dto)
        {
            return new BankAccountModel
            {
                CPF = dto.CPF,
                AccountType = dto.AccountType,
            };
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }

        public void YieldAccount(decimal yield)
        {
            Balance += yield;
        }
    }
}