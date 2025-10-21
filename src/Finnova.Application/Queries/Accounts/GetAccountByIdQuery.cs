using Finnova.Application.DTOs.Accounts;
using MediatR;

namespace Finnova.Application.Queries.Accounts
{
    public class GetAccountByIdQuery : IRequest<AccountDto?>
    {
        public int Id { get; set; }
    }
}