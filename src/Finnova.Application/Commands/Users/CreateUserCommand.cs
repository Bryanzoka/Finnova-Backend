using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<int>
    { 
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirmation { get; set; }
        public required string Code { get; set; }
    }
}