using Finnova.Application.Contracts;
using Finnova.Application.DTOs.Users;
using Finnova.Application.Exceptions;
using Finnova.Application.Queries.Users;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public GetClientByIdQueryHandler(IUserRepository userRepository, IUserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id != _userContext.UserId)
            {
                throw new ForbiddenException("unauthorized access");
            }

            var client = await _userRepository.GetByIdAsync(request.Id);

            if (client == null)
                return null;

            return new UserDto(client.Id, client.Name, client.Email);
        }
    }
}