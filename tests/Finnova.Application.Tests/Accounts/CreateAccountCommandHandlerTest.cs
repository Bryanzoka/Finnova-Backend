using Finnova.Application.Commands.Accounts;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Application.Handlers.Users.Accounts;
using Finnova.Domain.Entities;
using Finnova.Domain.Repositories;
using Moq;

namespace Finnova.Application.Tests.Accounts
{
    public class CreateAccountCommandHandlerTest
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateAccountCommandHandlerTest()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userContextMock = new Mock<IUserContext>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Should_Create_Account_When_Data_Is_Valid()
        {
            //arrange
            var user = User.Create("test", "test@mail.com", "password");

            _userRepositoryMock.Setup(ur => ur.GetByIdAsync(1)).ReturnsAsync(user);
            
            _userContextMock.Setup(uc => uc.UserId).Returns(1);

            _unitOfWorkMock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()));

            var handler = new CreateAccountCommandHandler(
                _accountRepositoryMock.Object,
                _userRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _userContextMock.Object
            );

            var command = new CreateAccountCommand
            {
                UserId = 1,
                Name = "account test",
                Type = Domain.Enums.AccountType.Checking
            };

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            Assert.IsType<int>(result);

            _accountRepositoryMock.Verify(ar => ar.AddAsync(It.IsAny<Account>()), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async Task Should_Throw_When_User_Not_Found()
        {
            //arrange
            _userRepositoryMock.Setup(ur => ur.GetByIdAsync(1)).ReturnsAsync((User?) null);

            var handler = new CreateAccountCommandHandler(
                _accountRepositoryMock.Object,
                _userRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _userContextMock.Object
            );

            var command = new CreateAccountCommand
            {
                UserId = 1,
                Name = "Account Test",
                Type = Domain.Enums.AccountType.Checking
            };

            //act
            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            //assert
           await Assert.ThrowsAsync<NotFoundException>(action);

           _accountRepositoryMock.Verify(ar => ar.AddAsync(It.IsAny<Account>()), Times.Never);

           _userRepositoryMock.Verify(ur => ur.GetByIdAsync(1), Times.Once);

           _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Should_Throw_Forbidden_When_User_Is_Not_Owner()
        {
            //arrange
            var user = User.Create("test", "test@mail.com", "password");
            
            _userRepositoryMock.Setup(ur => ur.GetByIdAsync(1)).ReturnsAsync(user);

            _userContextMock.Setup(uc => uc.UserId).Returns(2);

            var handler = new CreateAccountCommandHandler(
                _accountRepositoryMock.Object,
                _userRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _userContextMock.Object
            );

            var command = new CreateAccountCommand
            {
                UserId = 1,
                Name = "account test",
                Type = Domain.Enums.AccountType.Checking
            };

            //act
            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            //arrange
            await Assert.ThrowsAsync<ForbiddenException>(action);

           _accountRepositoryMock.Verify(ar => ar.AddAsync(It.IsAny<Account>()), Times.Never);

           _userRepositoryMock.Verify(ur => ur.GetByIdAsync(1), Times.Once);

           _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}