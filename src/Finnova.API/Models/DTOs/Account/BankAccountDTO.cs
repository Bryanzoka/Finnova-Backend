using FinnovaAPI.Enums;

namespace FinnovaAPI.Models.DTOs.Account
{
    public record BankAccountDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public EnumAccountType AccountType { get; set; } 
        public decimal Balance { get; set; }


        public static BankAccountDTO ToDTO(BankAccountModel model)
        {
            return new BankAccountDTO
            {
                Id = model.Id,
                ClientId = model.ClientId,
                AccountType = model.AccountType,
                Balance = model.Balance
            };
        }
    }
}