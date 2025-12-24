using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}