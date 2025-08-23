namespace FinnovaAPI.Models.DTOs
{
    public interface IClientInfo
    {
        string Cpf { get; }
        string Email { get; }
        string Phone { get; }
    }
}