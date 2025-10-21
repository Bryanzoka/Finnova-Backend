namespace Finnova.Domain.Services
{
    public interface IClientCanCreateAccountChecker
    {
        Task EnsureClientCanCreateAccount(int clientId);
    }
}