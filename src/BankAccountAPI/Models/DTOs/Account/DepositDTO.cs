using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models.DTOs
{
    public class DepositDTO
    {
        [Required(ErrorMessage = "Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
    }
}