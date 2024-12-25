using CommercePortal.Domain.ValueObjects;
using FluentValidation;

namespace CommercePortal.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="Money"/> value object.
/// </summary>
public class MoneyValidator : AbstractValidator<Money>
{
    public MoneyValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Amount must be greater than or equal to 0.");

        RuleFor(x => x.Currency)
            .NotNull()
                .WithMessage("Currency is required.");
    }
}