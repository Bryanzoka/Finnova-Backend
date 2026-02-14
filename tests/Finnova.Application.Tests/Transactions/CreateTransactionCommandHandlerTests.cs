using Finnova.Application.Commands.Transactions;
using Finnova.Application.Contracts;
using Finnova.Application.Exceptions;
using Finnova.Application.Handlers.Transactions;
using Finnova.Domain.Entities;
using Finnova.Domain.Exceptions;
using Finnova.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finnova.Application.Tests.Transactions
{
    public class CreateTransactionCommandHandlerTests
    {

        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateTransactionCommandHandlerTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _userContextMock = new Mock<IUserContext>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Should_Create_Transaction_When_Data_Is_Valid_For_Expense()
        {
            // arrange
            var account = Account.Create(1, "test", Domain.Enums.AccountType.Checking);
            account.ApplyTransaction(1000m, Domain.Enums.TransactionType.Income);

            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);

            _userContextMock.Setup(uc => uc.UserId).Returns(1);

            _unitOfWorkMock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()));

            var handler = new CreateTransactionCommandHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new CreateTransactionCommand
            {
                AccountId = 1,
                Amount = 100m,
                Type = Domain.Enums.TransactionType.Expense,
                Description = "transaction test",
                Date = DateTime.UtcNow
            };

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            account.Balance.Should().Be(900m);

            _transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Transaction>()), Times.Once);

            _accountRepositoryMock.Verify(ac => ac.Update(account), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Create_Transaction_When_Data_Is_Valid_For_Income()
        {
            // arrange
            var account = Account.Create(1, "test", Domain.Enums.AccountType.Checking);
            account.ApplyTransaction(1000m, Domain.Enums.TransactionType.Income);

            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);

            _userContextMock.Setup(uc => uc.UserId).Returns(1);

            _unitOfWorkMock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()));

            var handler = new CreateTransactionCommandHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new CreateTransactionCommand
            {
                AccountId = 1,
                Amount = 100m,
                Type = Domain.Enums.TransactionType.Income,
                Description = "transaction test",
                Date = DateTime.UtcNow
            };

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            account.Balance.Should().Be(1100m);

            _transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Transaction>()), Times.Once);

            _accountRepositoryMock.Verify(ac => ac.Update(account), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Insufficient_Balance_For_Expense()
        {
            // arrange
            var account = Account.Create(1, "test", Domain.Enums.AccountType.Checking);
            account.ApplyTransaction(100m, Domain.Enums.TransactionType.Income);

            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);

            _userContextMock.Setup(uc => uc.UserId).Returns(1);


            var handler = new CreateTransactionCommandHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new CreateTransactionCommand
            {
                AccountId = 1,
                Amount = 200m,
                Type = Domain.Enums.TransactionType.Expense,
                Description = "transaction test",
                Date = DateTime.UtcNow
            };

            // act
            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            // assert
            await action.Should().ThrowAsync<InsufficientBalanceException>();

            account.Balance.Should().Be(100m);

            _transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Transaction>()), Times.Never);

            _accountRepositoryMock.Verify(ar => ar.Update(It.IsAny<Account>()), Times.Never);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Should_Throw_When_Account_Not_Found()
        {
            // arrange
            Account? account = null;

            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);

            var handler = new CreateTransactionCommandHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new CreateTransactionCommand
            {
                AccountId = 1,
                Amount = 100m,
                Type = Domain.Enums.TransactionType.Expense,
                Description = "transaction test",
                Date = DateTime.UtcNow
            };

            // act
            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            // assert
            await action.Should().ThrowAsync<NotFoundException>();

            _transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Transaction>()), Times.Never);

            _accountRepositoryMock.Verify(ar => ar.Update(It.IsAny<Account>()), Times.Never);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Should_Throw_ForbiddenException_When_User_Does_Not_Own_Account()
        {
            // arrange
            var account = Account.Create(2, "test", Domain.Enums.AccountType.Checking);

            _accountRepositoryMock.Setup(ar => ar.GetByIdAsync(1)).ReturnsAsync(account);

            _userContextMock.Setup(uc => uc.UserId).Returns(1);

            var handler = new CreateTransactionCommandHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _userContextMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new CreateTransactionCommand
            {
                AccountId = 1,
                Amount = 100m,
                Type = Domain.Enums.TransactionType.Expense,
                Description = "transaction test",
                Date = DateTime.UtcNow
            };

            // act
            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            // assert

            await action.Should().ThrowAsync<ForbiddenException>();

            _transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Transaction>()), Times.Never);

            _accountRepositoryMock.Verify(ar => ar.Update(It.IsAny<Account>()), Times.Never);

            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}