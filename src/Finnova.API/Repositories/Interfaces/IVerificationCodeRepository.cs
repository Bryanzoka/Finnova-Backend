using FinnovaAPI.Models;

namespace FinnovaAPI.Repositories.Interfaces
{
    public interface IVerificationCodeRepository
    {
        Task<ClientVerificationCodeModel> GetCodeByEmail(string email);
        Task SaveCode(ClientVerificationCodeModel code);
        Task<bool> DeleteCode(ClientVerificationCodeModel code);
    }
}