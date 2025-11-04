using System.Text.Json.Serialization;
using Finnova.Domain.ValueObjects;

namespace Finnova.Application.DTOs.Accounts
{
    public record CreateAccountDto(
        int ClientId,
        AccountType Type,
        string Password,
        [property: JsonPropertyName("password_confirmation")] string PasswordConfirmation
    );
}