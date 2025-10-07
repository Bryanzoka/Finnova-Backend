using Finnova.Domain.ValueObjects;

namespace Finnova.Application.DTOs.Accounts
{
    public record AccountDto(
        int Id,
        int ClientId,
        AccountType Type,
        AccountStatus Status,
        decimal Balance
    );
}