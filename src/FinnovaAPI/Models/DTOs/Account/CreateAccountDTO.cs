using FinnovaAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinnovaAPI.Models.DTOs.Account
{
    public record CreateAccountDTO
    {
        [Required(ErrorMessage = "Client id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid id number")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "A Account Type is required")]
        [EnumDataType(typeof(EnumAccountType), ErrorMessage = "Invalid account type")]
        public EnumAccountType AccountType { get; set; }
    }
}