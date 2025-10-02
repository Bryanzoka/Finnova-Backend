using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class CreateClientCommand : IRequest<int>
    { 
        public required string Name { get; set; }
        public required string Cpf { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public required string Password_confirmation { get; set; }
        public required string Code { get; set; }
    }
}