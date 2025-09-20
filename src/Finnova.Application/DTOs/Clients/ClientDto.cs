namespace Finnova.Application.DTOs.Clients
{
    public record ClientDto(
        int Id,
        string Cpf,
        string Name,
        string Email,
        string Phone
    );
}