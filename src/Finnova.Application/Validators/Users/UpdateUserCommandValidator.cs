using Finnova.Application.Commands.Users;
using FluentValidation;

namespace Finnova.Application.Validators.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("name is required")
               .Length(2, 150).WithMessage("name must have minimum 2 and maximum 150 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email is required")
                .EmailAddress().WithMessage("invalid email format")
                .MaximumLength(150).WithMessage("email must have maximum 150 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("password is required");
        }
    }
}