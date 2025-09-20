using Finnova.Application.DTOs.Clients;
using MediatR;

namespace Finnova.Application.Queries.Clients
{
    public class GetClientByIdQuery : IRequest<ClientDto?>
    {
        public int Id { get; set; }
    }
}