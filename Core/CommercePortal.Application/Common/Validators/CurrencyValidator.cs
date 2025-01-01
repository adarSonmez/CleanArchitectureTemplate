using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.SmartEnums.Localizations;
using FluentValidation;

namespace CommercePortal.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="Currency"/> smart enum.
/// </summary>
public class CurrencyValidator : AbstractValidator<Currency>
{
    public CurrencyValidator()
    {
        RuleFor(x => x.IsoCode)
            .NotNull().NotEmpty().WithMessage("ISO code is required.")
            .Length(3).WithMessage("ISO code must be exactly 3 characters.")
            .Must(iso => Enumeration.GetAll<Currency>().Any(c => c.IsoCode.Equals(iso, StringComparison.OrdinalIgnoreCase)))
            .WithMessage(iso => $"Currency with ISO code '{iso}' is not supported.");

        RuleFor(x => x.Symbol)
            .NotNull().NotEmpty().WithMessage("Currency symbol is required.");
    }
}