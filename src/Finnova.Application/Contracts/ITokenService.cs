namespace Finnova.Application.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(int clientId, string email, string role);
    }
}