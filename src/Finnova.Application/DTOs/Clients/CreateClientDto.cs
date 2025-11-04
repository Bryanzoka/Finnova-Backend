using System.Text.Json.Serialization;

namespace Finnova.Application.DTOs.Clients
{
    public record CreateClientDto(
        string Name,
        string Cpf,
        string Email,
        string Phone,
        string Password,
        [property: JsonPropertyName("password_confirmation")]string PasswordConfirmation,
        string Code
    );
}