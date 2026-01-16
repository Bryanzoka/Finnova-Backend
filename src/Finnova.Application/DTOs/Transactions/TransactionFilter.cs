using Finnova.Domain.Enums;

namespace Finnova.Application.DTOs.Transactions
{
    public record TransactionFilter(
        TransactionType? Type,
        decimal? MinAmount,
        decimal? MaxAmount,
        DateTime? StartDate,
        DateTime? EndDate
    );
}