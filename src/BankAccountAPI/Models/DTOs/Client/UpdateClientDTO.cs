using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Models.DTOs.Client
{
    public class UpdateClientDTO
    {
        [StringLength(100, ErrorMessage = "The name entered is too long")]
        public string ClientName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string ClientEmail { get; set; }

        [StringLength(13, MinimumLength = 11, ErrorMessage = "Phone number must be between 11 and 13 digits long")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must be contain only numbers")]
        public string ClientTel { get; set; }
        
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
    }
}