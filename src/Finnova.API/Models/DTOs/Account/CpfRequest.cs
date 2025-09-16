using System.ComponentModel.DataAnnotations;

namespace FinnovaAPI.Models.DTOs.Account
{
    public record CpfRequest
    {
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string Cpf { get; set; }
    }
}