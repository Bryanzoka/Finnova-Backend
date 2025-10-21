using Finnova.Domain.ValueObjects;

namespace Finnova.Application.DTOs.Accounts
{
    public record CreateAccountDto(
        int ClientId,
        AccountType Type,
        string Password,
        string Password_confirmation
    );
}