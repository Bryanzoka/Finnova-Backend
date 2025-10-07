using Finnova.Application.DTOs.Accounts;
using Finnova.Application.DTOs.Clients;
using Finnova.Application.Queries.Accounts;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Accounts
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>?>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAllAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<AccountDto>?> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAllAsync();

            if (accounts == null)
                return null;

            return accounts.Select(a => new AccountDto(a.Id, a.ClientId, a.Type, a.Status, a.Balance)).ToList();
        }
    }
}