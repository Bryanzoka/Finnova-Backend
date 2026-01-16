namespace Finnova.Application.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string email, string role);
    }
}