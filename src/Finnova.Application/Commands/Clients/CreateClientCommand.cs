using MediatR;

namespace Finnova.Application.Commands.Clients
{
    public class CreateClientCommand : IRequest<int>
    { 
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}