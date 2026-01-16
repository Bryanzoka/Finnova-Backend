using Finnova.Application.DTOs.Transactions;
using Finnova.Domain.Enums;
using MediatR;

namespace Finnova.Application.Queries.Transactions
{
    public class GetAllTransactionsByAccountIdQuery : IRequest<List<TransactionDto>?>
    {
        public int Id { get; set; }
        public TransactionType? Type { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}