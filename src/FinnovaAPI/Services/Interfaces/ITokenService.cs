using FinnovaAPI.Models;

namespace FinnovaAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(BankClientModel bankClient);
    }
}