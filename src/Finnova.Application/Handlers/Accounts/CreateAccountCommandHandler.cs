using Finnova.Application.Commands.Accounts;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Domain.Aggregates;
using Finnova.Domain.Repositories;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Accounts
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IClientRepository clientRepository,
            IPasswordHasherService passwordHasher, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetByIdAsync(request.ClientId) == null)
                throw new NotFoundException("Client not found");

            var accounts = await _accountRepository.GetAllByClientIdAsync(request.ClientId);

            if (accounts != null)
            {
                if (accounts.Count >= 2)
                {
                    throw new InvalidOperationException("Client already has the maximum number of accounts");
                }

                foreach (var account in accounts)
                {
                    if (account.Type == request.Type)
                    {
                        throw new InvalidOperationException("Client already has an account of this type");
                    }
                }
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var newAccount = Account.Create(request.ClientId, request.Type, hashedPassword);
            await _accountRepository.AddAsync(newAccount);
            await _unitOfWork.CommitAsync(cancellationToken);

            return newAccount.Id;
        }
    }
}