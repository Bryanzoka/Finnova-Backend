using Finnova.Application.DTOs.Users;
using Finnova.Application.Queries.Users;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>?>
    {
        private readonly IUserRepository _userRepository;

        public GetAllClientsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>?> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var clients = await _userRepository.GetAllAsync();

            if (clients == null)
                return null;

            return clients.Select(c => new UserDto(c.Id, c.Name, c.Email)).ToList();
        }
    }
}