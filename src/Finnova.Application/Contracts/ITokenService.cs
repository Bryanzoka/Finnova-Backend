namespace Finnova.Application.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(int userId, string role);
        string GenerateRefreshToken();
    }
}