using Finnova.Domain.Enums;

namespace Finnova.Application.DTOs.Accounts
{
    public record UpdateAccountDto(
        string? Name,
        AccountType? Type,
        bool? IsActive
    );
}