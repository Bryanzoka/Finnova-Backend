using System.ComponentModel.DataAnnotations;

namespace FinnovaAPI.Models.DTOs.Account
{
    public class DeleteAccountDTO
    {
        [Required(ErrorMessage = "Account id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
    }
}