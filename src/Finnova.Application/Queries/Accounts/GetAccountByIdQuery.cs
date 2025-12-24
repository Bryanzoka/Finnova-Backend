using System.Text.Json.Serialization;
using Finnova.Application.DTOs.Accounts;
using MediatR;

namespace Finnova.Application.Queries.Accounts
{
    public class GetAccountByIdQuery : IRequest<AccountDto?>
    {
        public int Id { get; set; }
        public int TokenId { get; set; }
    }
}