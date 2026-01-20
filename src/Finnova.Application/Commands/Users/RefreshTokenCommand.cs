using Finnova.Application.DTOs.Users;
using MediatR;

namespace Finnova.Application.Commands.Users
{
    public class RefreshTokenCommand : IRequest<TokensDto>
    {
        public required string RefreshToken { get; set; }
    }
}