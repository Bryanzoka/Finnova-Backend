using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class LoginClientCommand : IRequest<string>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}