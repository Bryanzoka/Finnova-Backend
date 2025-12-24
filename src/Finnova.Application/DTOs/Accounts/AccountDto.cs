using System.Text.Json.Serialization;
using Finnova.Domain.Enums;

namespace Finnova.Application.DTOs.Accounts
{
    public record AccountDto(
        int Id,
        [property: JsonPropertyName("user_id")] int UserId,
        string Name,
        AccountType Type,
        decimal Balance
    );
}