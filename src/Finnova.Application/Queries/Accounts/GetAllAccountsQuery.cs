using Finnova.Application.DTOs.Accounts;
using Finnova.Domain.Enums;
using MediatR;

namespace Finnova.Application.Queries.Accounts
{
    public class GetAllAccountsQuery : IRequest<List<AccountDto>?>
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }
        public AccountType? Type { get; set; }
        public decimal? MinBalance { get; set; }
        public decimal? MaxBalance { get; set; }
    }
}