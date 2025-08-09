using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using BankAccountAPI.Models.DTOs.Account;
using BankAccountAPI.Controllers;
using Microsoft.AspNetCore.Identity;

namespace BankAccountAPI.Models
{
    public class BankAccountModel
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string Cpf { get; private set; }

        [Required(ErrorMessage = "Account type is required")]
        [EnumDataType(typeof(EnumAccountType), ErrorMessage = "Invalid account type")]
        public EnumAccountType AccountType { get; private set; } 
        public decimal Balance { get; private set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Password must be 4 digits long")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Password must contain only numbers")]
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastTransactionAt { get; private set; }

        [Required(ErrorMessage = "An account must be linked to user")]
        public virtual BankClientModel BankClient { get; private set; } 

        public BankAccountModel() {}

        [JsonConstructor]
        public BankAccountModel(int id, string cpf, EnumAccountType accounttype, string password)
        {
            Id = id;
            Cpf = cpf;
            AccountType = accounttype;
            Password = password;
            Balance = 0;
        }

        public static BankAccountModel ToModel(BankAccountDTO dto)
        {
            return new BankAccountModel
            {
                Id = dto.Id,
                Cpf = dto.Cpf,
                AccountType = dto.AccountType,
                Balance = dto.Balance
            };
        }

        public static BankAccountModel CreationDTOToModel(CreateAccountDTO dto)
        {
            return new BankAccountModel
            {
                Cpf = dto.Cpf,
                AccountType = dto.AccountType,
            };
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            LastTransactionAt = DateTime.UtcNow;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
            LastTransactionAt = DateTime.UtcNow;
        }

        public void YieldAccount(decimal yield)
        {
            Balance += yield;
            LastTransactionAt = DateTime.UtcNow;
        }
    }
}