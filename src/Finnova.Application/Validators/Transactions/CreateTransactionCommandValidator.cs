using Finnova.Application.Commands.Transactions;
using FluentValidation;

namespace Finnova.Application.Validators.Transactions
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(t => t.AccountId)
                .NotEmpty().WithMessage("account id is required")
                .GreaterThan(0).WithMessage("account id must be greater than zero");

            RuleFor(t => t.Amount)
                .NotEmpty().WithMessage("amount is required")
                .GreaterThan(0m).WithMessage("amount must be greater than zero");

            RuleFor(t => t.Type)
                .IsInEnum().WithMessage("invalid account type");

            RuleFor(t => t.Description)
                .Length(2, 255).WithMessage("name must have minimum 2 and maximum 255 characters")
                .When(t => !string.IsNullOrWhiteSpace(t.Description)).WithMessage("description cannot be empty if provided");

            RuleFor(t => t.Date)
                .NotEmpty().WithMessage("date is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("transaction date cannot be in the future");
        }
    }
}