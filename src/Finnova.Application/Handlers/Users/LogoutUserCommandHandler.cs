using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Unit>
    {
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUserCommandHandler(IRefreshTokenRepository tokenRepository, IUnitOfWork unitOfWork)
        {
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _tokenRepository.GetByTokenAsync(request.RefreshToken);

            if (token == null || !token.IsActive)
            {
                throw new UnauthorizedAccessException("invalid refresh token");
            }

            token.Revoke();

            _tokenRepository.Update(token);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}