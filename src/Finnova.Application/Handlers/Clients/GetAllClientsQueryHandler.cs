using Finnova.Application.DTOs.Clients;
using Finnova.Application.Queries.Clients;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Clients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientDto>?>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientDto>?> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllAsync();

            if (clients == null)
                return null;

            return clients.Select(c => new ClientDto(c.Id, c.Cpf, c.Name, c.Email, c.Phone)).ToList();
        }
    }
}