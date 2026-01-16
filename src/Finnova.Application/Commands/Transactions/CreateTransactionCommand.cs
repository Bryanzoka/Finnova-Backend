using Finnova.Domain.Enums;
using MediatR;

namespace Finnova.Application.Commands.Transactions
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public required TransactionType Type { get; set; }
        public string? Description { get; set; }
        public required DateTime Date { get; set; }
    }
}