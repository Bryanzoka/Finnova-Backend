using Finnova.Domain.Enums;

namespace Finnova.Application.DTOs.Accounts
{
    public record AccountFilter(
        bool? IsActive,
        AccountType? Type,
        decimal? MinBalance,
        decimal? MaxBalance
    );
}