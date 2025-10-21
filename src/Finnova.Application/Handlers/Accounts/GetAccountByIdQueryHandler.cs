using Finnova.Application.DTOs.Accounts;
using Finnova.Application.Queries.Accounts;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Accounts
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.Id);

            if (account == null)
                return null;

            return new AccountDto(account.Id, account.ClientId, account.Type, account.Status, account.Balance);
        }
    }
}