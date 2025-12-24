using Finnova.Domain.Enums;
using MediatR;

namespace Finnova.Application.Commands.Accounts
{
    public class CreateAccountCommand : IRequest<int>
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required AccountType Type { get; set; }
    }
}