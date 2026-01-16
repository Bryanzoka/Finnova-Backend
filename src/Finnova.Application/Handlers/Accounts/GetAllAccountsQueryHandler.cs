using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Accounts;
using Finnova.Application.Exceptions;
using Finnova.Application.Queries.Accounts;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Accounts
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;

        public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _userContext = userContext;
        }

        public async Task<List<AccountDto>?> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            if (request.Id != _userContext.UserId)
            {
                throw new ForbiddenException("unauthorized access");
            }

            var accounts = await _accountRepository.GetAllByUserIdAsync(request.Id, request.IsActive, request.Type, request.MinBalance, request.MaxBalance);

            if (accounts == null)
            {
                return null;
            }

            return accounts.Select(a => new AccountDto(a.Id, a.UserId, a.Name, a.Type, a.Balance)).ToList();
        }
    }
}