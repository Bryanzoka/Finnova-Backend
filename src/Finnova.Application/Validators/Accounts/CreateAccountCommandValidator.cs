using Finnova.Application.Commands.Accounts;
using FluentValidation;

namespace Finnova.Application.Validators.Accounts
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(a => a.UserId)
                .NotEmpty().WithMessage("user id is required")
                .GreaterThan(0).WithMessage("user id must be greater than zero");

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("name is required")
                .Length(2, 150).WithMessage("name must have minimum 2 and maximum 150 characters");

            RuleFor(a => a.Type)
                .IsInEnum().WithMessage("invalid account type");
        }
    }
}