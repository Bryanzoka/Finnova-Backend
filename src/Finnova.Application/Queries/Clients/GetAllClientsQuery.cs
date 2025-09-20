using Finnova.Application.DTOs.Clients;
using MediatR;

namespace Finnova.Application.Queries.Clients
{
    public class GetAllClientsQuery : IRequest<List<ClientDto>?>
    {

    }
}