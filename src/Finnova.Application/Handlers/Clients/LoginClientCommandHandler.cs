using Finnova.Application.Commands.Clients;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Clients
{
    public class LoginClientCommandHandler : IRequestHandler<LoginClientCommand, string>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasherService _passwordHasher;

        public LoginClientCommandHandler(IClientRepository clientRepository, ITokenService tokenService,
            IPasswordHasherService passwordHasher)
        {
            _clientRepository = clientRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<string> Handle(LoginClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByEmailAsync(request.Email) ?? throw new NotFoundException("Client not found");

            if (!_passwordHasher.VerifyPassword(request.Password, client.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return _tokenService.GenerateToken(client.Id, client.Email, "User");
        }
    }
}