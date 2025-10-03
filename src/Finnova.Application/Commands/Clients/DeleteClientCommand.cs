using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class DeleteClientCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}