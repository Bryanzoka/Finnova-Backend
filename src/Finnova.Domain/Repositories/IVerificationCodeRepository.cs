using Finnova.Domain.Aggregates;

namespace Finnova.Domain.Repositories
{
    public interface IVerificationCodeRepository
    {
        Task<VerificationCode?> GetByEmailAsync(string email);
        Task AddAsync(VerificationCode verificationCode);
        void Delete(VerificationCode verificationCode);
    }
}