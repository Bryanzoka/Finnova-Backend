using System.Text.Json.Serialization;

namespace Finnova.Application.DTOs.Users
{
    public record CreateUserDto(
        string Name,
        string Email,
        string Password,
        [property: JsonPropertyName("password_confirmation")]string PasswordConfirmation,
        string Code
    );
}