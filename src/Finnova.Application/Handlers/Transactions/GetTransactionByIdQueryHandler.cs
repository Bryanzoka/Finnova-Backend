using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Transactions;
using Finnova.Application.Exceptions;
using Finnova.Application.Queries.Transactions;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Transactions
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserContext userContext)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _userContext = userContext;
        }

        public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);

            if (transaction == null)
            {
                return null;
            }

            var account = await _accountRepository.GetByIdAsync(transaction.AccountId) ?? throw new NotFoundException("account not found");

            if (account.UserId != _userContext.UserId)
            {
                throw new ForbiddenException("unauthorized access");
            }

            return new TransactionDto(transaction.Id, transaction.AccountId, transaction.Amount, transaction.Type, transaction.Description, transaction.Date);
        }
    }
}