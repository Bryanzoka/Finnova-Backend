using Finnova.Application.DTOs.Clients;
using FluentValidation;

namespace Finnova.Application.Validators.Clients
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required")
               .Length(2, 150).WithMessage("Name must have minimum 2 and maximum 150 characters");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF is required")
                .Length(11).WithMessage("CPF must have exactly 11 digits")
                .Matches(@"^\d+$").WithMessage("CPF must contain only numbers");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must have maximum 255 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Length(11, 13).WithMessage("Phone must have between 11 and 13 digits")
                .Matches(@"^\d+$").WithMessage("Phone must contain only numbers");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must have at least 8 characters")
                .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
                .WithMessage("Password must have at least 1 uppercase letter, 1 number, and 1 symbol");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password).WithMessage("Password confirmation does not match the password");


            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Verification code is required")
                .Length(6).WithMessage("Code must have exactly 6 digits")
                .Matches(@"^\d\d+$").WithMessage("Code must contain only numbers");
        }
    }
}