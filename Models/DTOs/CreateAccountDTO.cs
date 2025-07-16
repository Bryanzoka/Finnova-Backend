using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Enums;
using BankAccountAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Models.DTOs
{
    public record CreateAccountDTO
    {
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A Account Type is required")]
        [EnumDataType(typeof(EnumAccountType), ErrorMessage = "Invalid account type")]
        public EnumAccountType AccountType { get; set; }
    }
}