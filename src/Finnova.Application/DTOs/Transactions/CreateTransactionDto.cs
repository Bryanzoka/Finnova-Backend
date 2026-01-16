using Finnova.Domain.Enums;

namespace Finnova.Application.DTOs.Transactions
{
    public class CreateTransactionDto
    {
        public int AccountId { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public string? Description { get; private set; }
        public DateTime Date { get; private set; }
    }
}