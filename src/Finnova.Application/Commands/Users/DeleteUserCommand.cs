using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}