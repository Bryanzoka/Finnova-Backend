using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class RequestVerificationCodeCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}