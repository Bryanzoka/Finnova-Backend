using System.Text.Json.Serialization;

namespace Finnova.Application.DTOs.Users
{
    public record RefreshTokenDto(
        [property: JsonPropertyName("refresh_token")] string RefreshToken
    );
}