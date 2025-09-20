using Finnova.Application.Commands.Clients;
using FinnovaAPI.Repositories;
using Finnova.Application.Contracts;
using MediatR;
using Finnova.Domain.Repositories;
using Finnova.Domain.Aggregates;

namespace Finnova.Application.Handlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientCommandHandler(IClientRepository clientRepository, IPasswordHasherService passwordHasher,
            IVerificationCodeRepository verificationCodeRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
            _verificationCodeRepository = verificationCodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.CpfExistsAsync(request.Cpf))
                throw new InvalidOperationException("Client with this CPF already exists");

            if (await _clientRepository.EmailExistsAsync(request.Email))
                throw new InvalidOperationException("Client with this e-mail already exists");

            if (await _clientRepository.PhoneExistsAsync(request.Phone))
                throw new InvalidOperationException("Client with this phone already exists");

            var verificationCode = await _verificationCodeRepository.GetByEmailAsync(request.Email) ?? throw new KeyNotFoundException("verification code not requested");

            verificationCode.Validate(request.Code);

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var client = Client.Create(request.Name, request.Cpf, request.Email, request.Phone, hashedPassword);

            await _clientRepository.AddAsync(client);
            _verificationCodeRepository.Delete(verificationCode);

            await _unitOfWork.CommitAsync(cancellationToken);

            return client.Id;
        }
    }
}