using Finnova.Application.Commands.Accounts;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users.Accounts
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(request.UserId) == null)
            {
                throw new NotFoundException("user not found");
            }

            if (request.UserId != _userContext.UserId)
            {
                throw new ForbiddenException("invalid operation");
            }

            var account = Account.Create(request.UserId, request.Name, request.Type);

            await _accountRepository.AddAsync(account);
            await _unitOfWork.CommitAsync(cancellationToken);

            return account.Id;
        }
    }
}