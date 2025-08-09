using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models.DTOs.Account
{
    public class CpfRequest
    {
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string Cpf { get; set; }
    }
}