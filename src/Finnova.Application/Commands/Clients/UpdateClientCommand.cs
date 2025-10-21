using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class UpdateClientCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public required string Password { get; set; }
    }
}