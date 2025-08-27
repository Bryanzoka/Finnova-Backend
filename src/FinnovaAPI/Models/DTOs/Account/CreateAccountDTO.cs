using FinnovaAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinnovaAPI.Models.DTOs.Account
{
    public record CreateAccountDTO
    {
        [JsonIgnore]
        [Required(ErrorMessage = "Client id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid id number")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "A Account Type is required")]
        [EnumDataType(typeof(EnumAccountType), ErrorMessage = "Invalid account type")]
        public EnumAccountType AccountType { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Password must be 4 digits long")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Password must contain only numbers")]
        public string Password { get; set; }
    }
}