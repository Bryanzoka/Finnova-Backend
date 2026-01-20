using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Users;
using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class ResfreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokensDto>
    {
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public ResfreshTokenCommandHandler(IRefreshTokenRepository tokenRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _tokenRepository = tokenRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _tokenRepository.GetByTokenAsync(request.RefreshToken);
            
            if (token == null || !token.IsActive)
            {
                throw new UnauthorizedAccessException("invalid refresh token");
            }

            token.Revoke();

            var newRefreshToken = RefreshToken.Create(token.UserId, _tokenService.GenerateRefreshToken());

            _tokenRepository.Update(token);
            await _tokenRepository.AddAsync(newRefreshToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new TokensDto(_tokenService.GenerateAccessToken(token.UserId, "user"), newRefreshToken.Token);
        }
    }
}