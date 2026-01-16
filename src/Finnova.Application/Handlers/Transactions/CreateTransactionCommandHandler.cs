using Finnova.Domain.Entities;
using Finnova.Application.Commands.Transactions;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Transactions
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;    
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId) ?? throw new NotFoundException("account not found");

            if (account.UserId != _userContext.UserId)
            {
                throw new ForbiddenException("unauthorized access");
            }

            var transaction = Transaction.Create(request.AccountId, request.Amount, request.Type, request.Description, request.Date);

            account.ApplyTransaction(request.Amount, request.Type);
            
            await _transactionRepository.AddAsync(transaction);
            _accountRepository.Update(account);

            await _unitOfWork.CommitAsync(cancellationToken);

            return transaction.Id;
        }
    }
}