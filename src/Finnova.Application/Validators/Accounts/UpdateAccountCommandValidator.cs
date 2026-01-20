using Finnova.Application.Commands.Accounts;
using FluentValidation;

namespace Finnova.Application.Validators.Accounts
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(a => a.Id)
                .GreaterThan(0).WithMessage("id must be greater than zero");

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("name is required")
                .Length(2, 150).WithMessage("name must have minimum 2 and maximum 150 characters")
                .When(a => a.Name != null);

            RuleFor(a => a.Type)
                .IsInEnum().WithMessage("invalid account type")
                .When(a => a.Type.HasValue);

            RuleFor(a => a.IsActive)
                .NotNull()
                .When(a => a.IsActive.HasValue);      
        }
    }
}