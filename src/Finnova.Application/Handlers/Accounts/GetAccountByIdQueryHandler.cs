using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Accounts;
using Finnova.Application.Exceptions;
using Finnova.Application.Queries.Accounts;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users.Accounts
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _userContext = userContext;
        }

        public async Task<AccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.Id);

            if (account == null)
            {
                return null;
            }

            if (account.UserId != _userContext.UserId)
            {
                throw new ForbiddenException("you are not authorized to acess this account");
            }

            return new AccountDto(account.Id, account.UserId, account.Name, account.Type, account.Balance);
        }
    }
}