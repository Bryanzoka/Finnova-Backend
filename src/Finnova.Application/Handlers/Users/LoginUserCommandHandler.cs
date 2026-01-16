using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class LoginClientCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasherService _passwordHasher;

        public LoginClientCommandHandler(IUserRepository userRepository, ITokenService tokenService,
            IPasswordHasherService passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email) ?? throw new NotFoundException("user not found");

            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("invalid credentials");
            }

            return _tokenService.GenerateToken(user.Id, user.Email, "User");
        }
    }
}