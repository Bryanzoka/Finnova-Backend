using Finnova.Application.DTOs.Accounts;
using MediatR;

namespace Finnova.Application.Queries.Accounts
{
    public class GetAllAccountsQuery : IRequest<List<AccountDto?>>
    {
        
    }
}