using Finnova.Domain.Enums;
using MediatR;

namespace Finnova.Application.Commands.Accounts
{
    public class UpdateAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AccountType? Type { get; set; }
        public bool? IsActive { get; set; } 
    }
}