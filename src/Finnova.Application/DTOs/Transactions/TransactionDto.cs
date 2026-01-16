using System.Text.Json.Serialization;
using Finnova.Domain.Enums;

namespace Finnova.Application.DTOs.Transactions
{
    public record TransactionDto(
        int Id,
        [property: JsonPropertyName("account_id")] int AccountId,
        decimal Amount,
        TransactionType Type,
        string? Description,
        DateTime Date
    );
}