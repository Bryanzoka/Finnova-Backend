using System.Text.Json.Serialization;
using Finnova.Domain.ValueObjects;
using MediatR;

namespace Finnova.Application.Commands.Accounts
{
    public class CreateAccountCommand : IRequest<int>
    {
        public required int ClientId { get; set; }
        public required AccountType Type { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirmation { get; set; }
    }
}