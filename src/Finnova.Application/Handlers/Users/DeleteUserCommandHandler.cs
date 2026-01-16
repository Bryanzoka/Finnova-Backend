using Finnova.Application.Commands.Users;
using Finnova.Application.Contracts;
using Finnova.Domain.Repositories;
using MediatR;

namespace Finnova.Application.Handlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"user with id: {request.Id} not found");

            _userRepository.Delete(user);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}