using System.Text.Json.Serialization;
using Finnova.Domain.Enums;

public record CreateAccountDto(
    [property: JsonPropertyName("user_id")] int UserId,
    string Name,
    AccountType Type 
);