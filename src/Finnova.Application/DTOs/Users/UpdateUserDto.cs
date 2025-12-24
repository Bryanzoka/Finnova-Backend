namespace Finnova.Application.DTOs.Users
{
    public record UpdateUserDto(
        string Name,
        string Email,
        string Password
    );
}