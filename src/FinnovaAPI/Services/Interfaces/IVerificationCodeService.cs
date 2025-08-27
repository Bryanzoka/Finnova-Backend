using FinnovaAPI.Models;

namespace FinnovaAPI.Services.Interfaces
{
    public interface IVerificationCodeService
    {
        Task<ClientVerificationCodeModel> GetCodeByEmail(string email);
        Task SendAndSaveCode(string email);
        Task ValidateCode(ClientVerificationCodeModel code);
        Task<bool> DeleteCode(string email);
    }
}