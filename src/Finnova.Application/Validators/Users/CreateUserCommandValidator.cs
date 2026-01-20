using Finnova.Application.Commands.Users;
using FluentValidation;

namespace Finnova.Application.Validators.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("name is required")
               .Length(2, 150).WithMessage("name must have minimum 2 and maximum 150 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email is required")
                .EmailAddress().WithMessage("invalid email format")
                .MaximumLength(150).WithMessage("email must have maximum 150 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("password is required")
                .MinimumLength(8).WithMessage("password must have at least 8 characters")
                .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
                .WithMessage("password must have at least 1 uppercase letter, 1 number, and 1 symbol");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password).WithMessage("password confirmation does not match the password");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("verification code is required")
                .Length(6).WithMessage("code must have exactly 6 digits")
                .Matches(@"^\d\d+$").WithMessage("code must contain only numbers");
        }
    }
}