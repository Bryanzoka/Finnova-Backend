using Finnova.Application.Commands.Clients;
using Finnova.Application.Contracts;
using Finnova.Domain.Aggregates;
using Finnova.Domain.Repositories;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers
{
    public class RequestVerificationCodeCommandHandler : IRequestHandler<RequestVerificationCodeCommand, Unit>
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestVerificationCodeCommandHandler(IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService, IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RequestVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.EmailExistsAsync(request.Email))
                throw new InvalidOperationException("Client with this e-mail already exists");

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