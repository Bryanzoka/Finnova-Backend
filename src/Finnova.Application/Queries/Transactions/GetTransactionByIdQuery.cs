using Finnova.Application.DTOs.Transactions;
using MediatR;

namespace Finnova.Application.Queries.Transactions
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto?>
    {
        public int Id { get; set; }
    }
}