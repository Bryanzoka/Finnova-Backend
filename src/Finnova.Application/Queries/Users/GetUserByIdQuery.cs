using Finnova.Application.DTOs.Users;
using MediatR;

namespace Finnova.Application.Queries.Users
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public int Id { get; set; }
    }
}