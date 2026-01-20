using Finnova.Application.DTOs.Users;
using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<TokensDto>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}