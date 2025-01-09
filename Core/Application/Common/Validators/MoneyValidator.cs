using CleanArchitectureTemplate.Domain.ValueObjects;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="Money"/> value object.
/// </summary>
public class MoneyValidator : AbstractValidator<Money>
{
    public MoneyValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
                .WithMessage("Amount is required.")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Amount must be greater than or equal to 0.");

        RuleFor(x => x!.Currency)
            .NotNull().WithMessage("Currency is required.")
            .SetValidator(new CurrencyValidator());
    }
}