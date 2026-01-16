using Finnova.Application.Commands.Accounts;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Accounts
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository, IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("account not found");

            if (account.UserId != _userContext.UserId)
            {
                throw new ForbiddenException("unauthorized access");
            }

            _accountRepository.Delete(account);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}