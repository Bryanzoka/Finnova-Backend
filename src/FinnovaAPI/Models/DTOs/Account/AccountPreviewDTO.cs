using FinnovaAPI.Enums;

namespace FinnovaAPI.Models.DTOs.Account
{
    public class AccountPreviewDTO
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public EnumAccountType AccountType { get; set; }
    }
}