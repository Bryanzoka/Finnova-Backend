using Finnova.Application.Queries.Transactions;
using FluentValidation;

namespace Finnova.Application.Validators.Transactions
{
    public class GetAllTransactionsByUserIdQueryValidator : AbstractValidator<GetAllTransactionsByUserIdQuery>
    {
        public GetAllTransactionsByUserIdQueryValidator()
        {
            RuleFor(t => t.UserId)
                .GreaterThan(0).WithMessage("user id must be greater than zero");

            RuleFor(t => t.Type)
                .IsInEnum().WithMessage("invalid transaction type")
                .When(t => t.Type.HasValue);

            RuleFor(t => t.MinAmount)
                .GreaterThanOrEqualTo(0m).WithMessage("minimum amount must be zero or greater")
                .When(t => t.MinAmount.HasValue);

            RuleFor(t => t.MaxAmount)
                .GreaterThan(t => t.MinAmount).WithMessage("maximum amount must be greater than minimum amount")
                .When(t => t.MaxAmount.HasValue);

            RuleFor(t => t.StartDate)
                .LessThanOrEqualTo(t => t.EndDate!.Value).WithMessage("start date must be less than or equal to end date")
                .When(t => t.StartDate.HasValue && t.EndDate.HasValue);

            RuleFor(t => t.EndDate)
                .GreaterThanOrEqualTo(t => t.StartDate!.Value).WithMessage("end date must be greater than or equal to start date")
                .When(t => t.StartDate.HasValue && t.EndDate.HasValue);
        }
    }
}