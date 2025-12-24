using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientCommandHandler(IUserRepository userRepository, IPasswordHasherService passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"user not found");

            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("invalid credentials");
            }

            if (request.Email != null && request.Email != user.Email)
            {
                if (await _userRepository.EmailExistsAsync(request.Email))
                {
                    throw new InvalidOperationException("user with this email already exists");
                }
            }

            user.Update(
                request.Name ?? user.Name,
                request.Email ?? user.Email,
                request.Password ?? user.Password
            );

            _userRepository.Update(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}