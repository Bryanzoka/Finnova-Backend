namespace Finnova.Application.DTOs.Clients
{
    public record UpdateClientDto(
        string Name,
        string Email,
        string Phone,
        string Password
    );
}