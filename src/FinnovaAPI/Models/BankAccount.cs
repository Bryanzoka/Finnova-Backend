using System.ComponentModel.DataAnnotations;
using FinnovaAPI.Enums;
using System.Text.Json.Serialization;
using FinnovaAPI.Models.DTOs.Account;

namespace FinnovaAPI.Models
{
    public class BankAccountModel
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Client id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid id number")]
        public int ClientId { get; private set; }

        [Required(ErrorMessage = "Account type is required")]
        [EnumDataType(typeof(EnumAccountType), ErrorMessage = "Invalid account type")]
        public EnumAccountType AccountType { get; private set; } 
        public decimal Balance { get; private set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Password must be 4 digits long")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Password must contain only numbers")]
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastTransactionAt { get; private set; }

        [Required(ErrorMessage = "An account must be linked to user")]
        public virtual BankClientModel BankClient { get; private set; } 

        public BankAccountModel() {}

        [JsonConstructor]
        public BankAccountModel(int id, int clientId, EnumAccountType accounttype, string password)
        {
            Id = id;
            ClientId = clientId;
            AccountType = accounttype;
            Password = password;
            Balance = 0;
        }

        public static BankAccountModel ToModel(BankAccountDTO dto)
        {
            return new BankAccountModel
            {
                Id = dto.Id,
                ClientId = dto.ClientId,
                AccountType = dto.AccountType,
                Balance = dto.Balance
            };
        }

        public static BankAccountModel CreationDTOToModel(CreateAccountDTO dto)
        {
            return new BankAccountModel
            {
                ClientId = dto.ClientId,
                AccountType = dto.AccountType,
                Password = dto.Password
            };
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            LastTransactionAt = DateTime.UtcNow;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
            LastTransactionAt = DateTime.UtcNow;
        }

        public void YieldAccount(decimal yield)
        {
            Balance += yield;
            LastTransactionAt = DateTime.UtcNow;
        }
    }
}