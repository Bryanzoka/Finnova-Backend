using System.Text.Json.Serialization;

namespace Finnova.Application.DTOs.Users
{
    public record TokensDto(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("refresh_token")] string RefreshToken
    );
}