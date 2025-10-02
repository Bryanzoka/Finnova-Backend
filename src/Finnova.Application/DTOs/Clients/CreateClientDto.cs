namespace Finnova.Application.DTOs.Clients
{
    public record CreateClientDto(
        string Name,
        string Cpf,
        string Email,
        string Phone,
        string Password,
        string Password_confirmation,
        string Code
    );
}