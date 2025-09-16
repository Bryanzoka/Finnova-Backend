using System.ComponentModel.DataAnnotations;

namespace FinnovaAPI.Models.DTOs.Client
{
    public record RegisterClientDTO : IClientInfo
    {
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "The entered name is to long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Phone number must be between 11 and 13 digits long")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only numbers")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string Password_confirmation { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(6, ErrorMessage = "Code must be only 6 characters long")]
        public string Code { get; set; }
    }
}