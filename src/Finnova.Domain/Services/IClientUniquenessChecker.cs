namespace Finnova.Domain.Services
{
    public interface IClientUniquenessChecker
    {
        Task EnsureAllIsUniqueAsync(string cpf, string email, string phone);
        Task EnsureCpfIsUniqueAsync(string cpf);
        Task EnsureEmailIsUniqueAsync(string email);
        Task EnsurePhoneIsUniqueAsync(string phone);
    }
}