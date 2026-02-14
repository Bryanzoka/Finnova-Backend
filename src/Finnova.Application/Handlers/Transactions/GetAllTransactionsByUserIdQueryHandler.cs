using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Transactions;
using Finnova.Application.Exceptions;
using Finnova.Application.Queries.Transactions;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Transactions
{
    public class GetAllTransactionsByUserIdQueryHandler : IRequestHandler<GetAllTransactionsByUserIdQuery, List<TransactionDto>?>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserContext _userContext;

        public GetAllTransactionsByUserIdQueryHandler(ITransactionRepository transactionRepository, IUserContext userContext)
        {
            _transactionRepository = transactionRepository;
            _userContext = userContext;
        }
        public async Task<List<TransactionDto>?> Handle(GetAllTransactionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userContext.UserId)
            {
                throw new ForbiddenException("unauthorized access");
            }

            if (request.StartDate.HasValue)
            {
                request.StartDate = DateTime.SpecifyKind(request.StartDate.Value, DateTimeKind.Utc);
            }

            if (request.EndDate.HasValue)
            {
                request.EndDate = DateTime.SpecifyKind(request.EndDate.Value, DateTimeKind.Utc);
            }

            var transactions = await _transactionRepository.GetAllByUserIdAsync(request.UserId, request.Type, request.MinAmount, request.MaxAmount, request.StartDate, request.EndDate);

            if (transactions == null)
            {
                return null;
            }

            return transactions.Select(t => new TransactionDto(t.Id, t.AccountId, t.Amount, t.Type, t.Description, t.Date)).ToList();
        }
    }
}