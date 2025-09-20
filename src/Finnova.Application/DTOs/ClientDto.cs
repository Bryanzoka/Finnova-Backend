namespace Finnova.Application.DTOs
{
    public record ClientDto(
        int Id,
        string Cpf,
        string Name,
        string Email,
        string Phone
    );
}