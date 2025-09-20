using Finnova.Application.DTOs.Clients;
using Finnova.Application.Queries.Clients;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Clients
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id);

            if (client == null)
                return null;

            return new ClientDto(client.Id, client.Cpf, client.Name, client.Email, client.Phone);
        }
    }
}