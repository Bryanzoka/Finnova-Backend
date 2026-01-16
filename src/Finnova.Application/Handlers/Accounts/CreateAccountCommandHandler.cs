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

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(request.UserId) == null)
            {
                throw new NotFoundException("user not found");
            }

            var account = Account.Create(request.UserId, request.Name, request.Type);

            await _accountRepository.AddAsync(account);
            await _unitOfWork.CommitAsync();

            return account.Id;
        }
    }
}