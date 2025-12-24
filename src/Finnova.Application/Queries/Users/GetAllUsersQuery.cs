using Finnova.Application.DTOs.Users;
using MediatR;

namespace Finnova.Application.Queries.Users
{
    public class GetAllUsersQuery : IRequest<List<UserDto>?>
    {
        
    }
}