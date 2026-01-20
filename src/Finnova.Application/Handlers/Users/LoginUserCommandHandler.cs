using System.Security.Cryptography;
using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Users;
using Finnova.Application.Exceptions;
using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class LoginClientCommandHandler : IRequestHandler<LoginUserCommand, TokensDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasherService _passwordHasher;

        public LoginClientCommandHandler(IUserRepository userRepository, IRefreshTokenRepository tokenRepository, ITokenService tokenService, IPasswordHasherService passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<TokensDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email) ?? throw new NotFoundException("user not found");

            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("invalid credentials");
            }

            var refreshToken = RefreshToken.Create(user.Id, _tokenService.GenerateRefreshToken());

            await _tokenRepository.AddAsync(refreshToken);

            await _unitOfWork.CommitAsync(cancellationToken);
            
            return new TokensDto(_tokenService.GenerateAccessToken(user.Id, "user"), refreshToken.Token);
        }
    }
}