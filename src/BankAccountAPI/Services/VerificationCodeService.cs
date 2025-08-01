using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using BankAccountAPI.Repositories;
using BankAccountAPI.Repositories.Interfaces;
using BankAccountAPI.Services.Interfaces;

namespace BankAccountAPI.Services
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

        public async Task SendAndSaveCode(ClientVerificationCodeModel code)
        {
            if (string.IsNullOrEmpty(code.Code) || code.Code.Length != 6)
            {
                throw new ArgumentException("Invalid code format");
            }

            if (await CodeExistsByEmail(code.Email))
            {
                await DeleteCode(code.Email);
            }

            await _emailService.SendVerificationCode(code.Email, code.Code);
            await _verificationCodeRepository.SaveCode(code);
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
    }
}