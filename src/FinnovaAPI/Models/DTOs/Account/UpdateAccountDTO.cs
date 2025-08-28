using System.ComponentModel.DataAnnotations;

namespace FinnovaAPI.Models.DTOs.Account
{
    public class UpdateAccountDTO
    {
        [Required(ErrorMessage = "Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Password must be 4 digits long")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Password must contain only numbers")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The new password is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "New password must be 4 digits long")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "New password must contain only numbers")]
        public string NewPassword { get; set; }
    }
}