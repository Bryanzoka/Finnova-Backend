using System.ComponentModel.DataAnnotations;

namespace FinnovaAPI.Models.DTOs.Client
{
    public class UpdateClientDTO
    {
        [StringLength(100, ErrorMessage = "The name entered is too long")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [StringLength(13, MinimumLength = 11, ErrorMessage = "Phone number must be between 11 and 13 digits long")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must be contain only numbers")]
        public string Phone { get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string Password_confirmation { get; set; }
    }
}