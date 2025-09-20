using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class RequestVerificationCodeCommand : IRequest<Unit>
    {
        public required string Email { get; set; }
    }
}