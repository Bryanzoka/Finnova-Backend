using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class RequestVerificationCodeCommand : IRequest<Unit>
    {
        public required string Email { get; set; }
    }
}