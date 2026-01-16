using Finnova.Domain.Enums;
using Finnova.Domain.Exceptions;

namespace Finnova.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }
        public int AccountId { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public string? Description { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Transaction(int accountId, decimal amount, TransactionType type, string? description, DateTime date)
        {
            AccountId = accountId;
            Amount = amount;
            Type = type;
            Description = description;
            Date = date;
            CreatedAt = DateTime.UtcNow;
        }

        public static Transaction Create(int accountId, decimal amount, TransactionType type, string? description, DateTime date)
        {
            if (amount <= 0m)
            {
                throw new DomainException("amount must be greater than zero");
            }

            if (description != null && string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException("description cannot be empty");
            }

            return new Transaction(accountId, amount, type, description, date);
        }
    }
}