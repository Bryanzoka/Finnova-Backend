using Finnova.Application.Commands.Clients;
using Finnova.Application.Contracts;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Clients
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id) ?? throw new AppDomainUnloadedException($"Client with id: {request.Id} not found");

            _clientRepository.Delete(client);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}