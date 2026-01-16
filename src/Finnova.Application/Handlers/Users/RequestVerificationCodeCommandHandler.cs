using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Clients
{
    public class RequestVerificationCodeCommandHandler : IRequestHandler<RequestVerificationCodeCommand, Unit>
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestVerificationCodeCommandHandler(IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RequestVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailExistsAsync(request.Email))
                throw new InvalidOperationException("user with this email already exists");

            var verificationCode = VerificationCode.Create(request.Email);

            var existingCode = await _verificationCodeRepository.GetByEmailAsync(request.Email);

            if (existingCode != null)
            {
                _verificationCodeRepository.Delete(existingCode);
            }

            await _verificationCodeRepository.AddAsync(verificationCode);

            await _unitOfWork.CommitAsync(cancellationToken);

            await _emailService.SendEmailAsync(request.Email, "Finnova Bank verification code",
                $"Your verification code is {verificationCode.Code} and expires in 5 minutes");

            return Unit.Value;
        }
    }
}