using Finnova.Application.Queries.Accounts;
using FluentValidation;

namespace Finnova.Application.Validators.Accounts
{
    public class GetAllAccountsByUserIdQueryValidator : AbstractValidator<GetAllAccountsByUserIdQuery>
    {
        public GetAllAccountsByUserIdQueryValidator()
        {
            RuleFor(a => a.Id)
                .GreaterThan(0).WithMessage("user id must be greater than zero");

            RuleFor(a => a.IsActive)
                .NotNull()
                .When(a => a.IsActive.HasValue);

            RuleFor(a => a.Type)
                .IsInEnum().WithMessage("invalid account type")
                .When(a => a.Type.HasValue);
                
            RuleFor(a => a.MinBalance)
                .GreaterThan(0).WithMessage("minimum balance must be greater than zero")
                .When(a => a.MinBalance.HasValue);

            RuleFor(a => a.MaxBalance)
                .GreaterThan(0).WithMessage("maximum balance must be greater than zero")
                .When(a => a.MaxBalance.HasValue);
        }
    }
}