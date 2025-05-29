using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Models.DTOs
{
    public class UpdateClientDTO
    {
        [StringLength(100, ErrorMessage = "O nome inserido é muito longo")]
        public string ClientName { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string ClientEmail { get; set; }

        [StringLength(13, MinimumLength = 11, ErrorMessage = "O número de telefone deve conter entre 11 a 13 dígitos")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O número de telefone deve conter apenas números")]
        public string ClientTel { get; set; }
        
        [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres")]
        public string Password { get; set; }
    }
}