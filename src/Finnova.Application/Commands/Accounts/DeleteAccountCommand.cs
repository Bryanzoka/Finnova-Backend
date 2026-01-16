using MediatR;

namespace Finnova.Application.Commands.Accounts
{
    public class DeleteAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}