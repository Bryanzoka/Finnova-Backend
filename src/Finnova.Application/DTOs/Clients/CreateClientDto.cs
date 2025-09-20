namespace Finnova.Application.DTOs
{
    public record CreateClientDto(
        string Name,
        string Cpf,
        string Email,
        string Phone,
        string Password,
        string Code
    );
}