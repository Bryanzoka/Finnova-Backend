using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class LogoutUserCommand : IRequest<Unit>
    {
        public required string RefreshToken { get; set; }
    }
}