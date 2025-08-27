using System.Net.Mail;
using System.Security.Cryptography;
using FinnovaAPI.Models;
using FinnovaAPI.Repositories.Interfaces;
using FinnovaAPI.Services.Interfaces;

namespace FinnovaAPI.Services
{
    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;

        public VerificationCodeService(IVerificationCodeRepository verificationCodeRepository, IEmailService emailService)
        {
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
        }

        public async Task<ClientVerificationCodeModel> GetCodeByEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("Invalid email format");
            }

            var code = await _verificationCodeRepository.GetCodeByEmail(email) ?? throw new KeyNotFoundException("Code not found");

            return code;
        }

        public async Task SendAndSaveCode(string email)
        {
            var code = GenerateRandomCode();

            if (await CodeExistsByEmail(email))
            {
                await DeleteCode(email);
            }

            await _emailService.SendVerificationCode(email, code);
            
            var verificationCode = new ClientVerificationCodeModel(email, code);
            await _verificationCodeRepository.SaveCode(verificationCode);
        }

        public async Task ValidateCode(string email, string code)
        {
            var verificationCode = await GetCodeByEmail(email) ?? throw new KeyNotFoundException("No verification code found for this email");

            if (DateTime.UtcNow > verificationCode.Expiration)
            {
                await DeleteCode(verificationCode.Email);
                throw new InvalidOperationException("Verification code has expired");
            }
                
            if (verificationCode.Code != code)
            {
                throw new UnauthorizedAccessException("Invalid verification code");
            }
        }

        public async Task<bool> DeleteCode(string email)
        {
            await GetCodeByEmail(email);

            return await _verificationCodeRepository.DeleteCode(email);
        }

        private async Task<bool> CodeExistsByEmail(string email)
        {
            try
            {
                await GetCodeByEmail(email);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private string GenerateRandomCode()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        }
    }
}