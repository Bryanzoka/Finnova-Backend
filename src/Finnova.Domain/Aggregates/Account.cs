using Finnova.Domain.Exceptions;
using Finnova.Domain.ValueObjects;

namespace Finnova.Domain.Aggregates
{
    public class Account
    {
        public int Id { get; private set; }
        public int ClientId { get; private set; }
        public AccountType Type { get; private set; }
        public AccountStatus Status { get; private set; }
        public decimal Balance { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Account(int clientId, AccountType type, string password)
        {
            if (clientId <= 0)
                throw new DomainException("Invalid client id");

            if (type != AccountType.Current && type != AccountType.Savings)
                throw new DomainException("Invalid account type");

            if (string.IsNullOrWhiteSpace(Password))
                throw new DomainException("Password is required");

            ClientId = clientId;
            Type = type;
            Password = password;
            Status = AccountStatus.Active;
            Balance = 0m;
            CreatedAt = DateTime.Now;
        }

        public static Account Create(int clientId, AccountType type, string password)
        {
            return new Account(clientId, type, password);
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new DomainException("amount must be greater than zero");

            if (Status != AccountStatus.Active)
                throw new DomainException("The account is not activated");

            Balance += amount;
            UpdatedAt = DateTime.Now;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new DomainException("amount must be greater than zero");

            if (Status != AccountStatus.Active)
                throw new DomainException("The account is not activated");

            if (Type == AccountType.Current)
            {
                if (amount > Balance)
                {
                    throw new InsufficientBalanceException("Insufficient balance");
                }

                if (amount > 10000m)
                {
                    throw new InvalidAccountOperationException("amount above permitted for this account type");
                }
            }
            else if (Type == AccountType.Savings)
            {
                if (amount > Balance)
                {
                    throw new InsufficientBalanceException("Insufficient balance");
                }

                if (amount > 5000m)
                {
                    throw new InvalidAccountOperationException("amount above permitted for this account type");
                }
            }
            else
            {
                throw new InvalidAccountOperationException("account type not specified");
            }

            Balance -= amount;
            UpdatedAt = DateTime.Now;
        }

        public void Active()
        {
            Status = AccountStatus.Active;
        }

        public void Freeze()
        {
            Status = AccountStatus.Frozen;
        }

        public void Disable()
        {
            Status = AccountStatus.Disabled;
        }
    }
}