using Finnova.Application.Commands.Accounts;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Application.Handlers.Accounts;
using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using MediatR;
using Moq;

namespace Finnova.Application.Tests.Accounts
{
    public class DeleteAccountCommandHandlerTest
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteAccountCommandHandlerTest()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _userContextMock = new Mock<IUserContext>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Should_Delete_When_User_Is_Owner()
        {
            var account = Account.Create(1, "test", Domain.Enums.AccountType.Checking);
            
            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);
            
            _userContextMock.Setup(uc => uc.UserId).Returns(1);

            var handler = new DeleteAccountCommandHandler(
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new DeleteAccountCommand
            {
                Id = 1
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result == Unit.Value);

            _accountRepositoryMock.Verify(ar => ar.Delete(It.IsAny<Account>()), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Forbidden_When_User_Is_Not_Owner()
        {
            var account = Account.Create(1, "test", Domain.Enums.AccountType.Checking);

            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);

            _userContextMock.Setup(uc => uc.UserId).Returns(2);

            var handler = new DeleteAccountCommandHandler(
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new DeleteAccountCommand
            {
                Id = 1
            };

            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            await Assert.ThrowsAsync<ForbiddenException>(action);

            _accountRepositoryMock.Verify(ar => ar.Delete(It.IsAny<Account>()), Times.Never);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}