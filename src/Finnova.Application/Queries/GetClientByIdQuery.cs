using Finnova.Application.DTOs;
using MediatR;

namespace Finnova.Application.Queries
{
    public class GetClientByIdQuery : IRequest<ClientDto>
    {
        public int Id { get; set; }
    }
}