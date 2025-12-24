using Finnova.Application.DTOs.Users;
using Finnova.Application.Queries.Users;
using FinnovaAPI.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public GetClientByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _userRepository.GetByIdAsync(request.Id);

            if (client == null)
                return null;

            return new UserDto(client.Id, client.Name, client.Email);
        }
    }
}