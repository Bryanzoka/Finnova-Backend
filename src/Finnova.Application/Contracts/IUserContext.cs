namespace Finnova.Application.Contracts
{
    public interface IUserContext
    {
        int UserId { get; }
        bool IsInRole(string role);
    }
}