using Finnova.Application.Commands.Clients;
using Finnova.Application.Contracts;
using Finnova.Domain.Services;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Clients
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Unit>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientUniquenessChecker _clientChecker;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IClientUniquenessChecker clientChecker,
            IPasswordHasherService passwordHasher, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _clientChecker = clientChecker;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Client with ID: {request.Id} not found");

            if (request.Email != null && request.Email != client.Email)
            {
                await _clientChecker.EnsureEmailIsUniqueAsync(request.Email);
            }

            if (request.Phone != null && request.Phone != client.Phone)
            {
                await _clientChecker.EnsurePhoneIsUniqueAsync(request.Phone);
            }

            client.Update(
                request.Name ?? client.Name,
                request.Email ?? client.Email,
                request.Phone ?? client.Phone
            );

            if (request.Password != null)
            {
                client.SetHashPassword(_passwordHasher.HashPassword(request.Password));
            }

            _clientRepository.Update(client);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}