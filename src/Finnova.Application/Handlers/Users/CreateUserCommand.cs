using FinnovaAPI.Repositories;
using Finnova.Application.Contracts;
using MediatR;
using Finnova.Domain.Repositories;
using Finnova.Domain.Entities;
using Finnova.Application.Commands.Users;

namespace Finnova.Application.Handlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasherService passwordHasher,
            IVerificationCodeRepository verificationCodeRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _verificationCodeRepository = verificationCodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailExistsAsync(request.Email))
            {
                throw new InvalidOperationException("user with this email already exists");
            }

            var verificationCode = await _verificationCodeRepository.GetByEmailAsync(request.Email) ?? throw new KeyNotFoundException("verification code not requested");

            verificationCode.Validate(request.Code);

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = User.Create(request.Name, request.Email, hashedPassword);

            await _userRepository.AddAsync(user);
            _verificationCodeRepository.Delete(verificationCode);

            await _unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }
    }
}