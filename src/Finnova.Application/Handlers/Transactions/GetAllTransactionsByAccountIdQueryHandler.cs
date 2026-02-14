using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Transactions;
using Finnova.Application.Exceptions;
using Finnova.Application.Queries.Transactions;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Transactions
{
    public class GetAllTransactionsByAccountIdQueryHandler : IRequestHandler<GetAllTransactionsByAccountIdQuery, List<TransactionDto>?>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;

        public GetAllTransactionsByAccountIdQueryHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserContext userContext)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _userContext = userContext;
        }

        public async Task<List<TransactionDto>?> Handle(GetAllTransactionsByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId) ?? throw new NotFoundException("account not found");

            if (account.UserId != _userContext.UserId)
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

            var transactions = await _transactionRepository.GetAllByAccountIdAsync(request.AccountId, request.Type, request.MinAmount, request.MaxAmount, request.StartDate, request.EndDate);

            if (transactions == null)
            {
                return null;
            }

            return transactions.Select(t => new TransactionDto(t.Id, t.AccountId, t.Amount, t.Type, t.Description, t.Date)).ToList();
        }
    }
}