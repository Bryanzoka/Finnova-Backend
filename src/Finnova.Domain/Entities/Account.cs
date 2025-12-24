using Finnova.Domain.Enums;
using Finnova.Domain.Exceptions;

namespace Finnova.Domain.Entities
{
    public class Account
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public AccountType Type { get; private set; }
        public decimal Balance { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Account(int userId, string name, AccountType type)
        {
            UserId = userId;
            Name = name;
            Type = type;
            Balance = 0m;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Account Create(int userId, string name, AccountType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("invalid account name");
            }

            return new Account(userId, name, type);
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("invalid account name");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow; 
        }
    }
}