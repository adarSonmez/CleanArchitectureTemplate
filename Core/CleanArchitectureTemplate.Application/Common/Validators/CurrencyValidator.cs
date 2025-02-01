using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using CleanArchitectureTemplate.Domain.Shared;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="Currency"/> smart enum.
/// </summary>
public class CurrencyValidator : AbstractValidator<Currency>
{
    public CurrencyValidator()
    {
        RuleFor(x => x)
            .NotNull()
                .WithMessage("Currency is required.")
            .Must(BeAValidCurrency)
                .WithMessage("The provided currency is not valid.");

        RuleFor(x => x.IsoCode)
            .NotEmpty()
                .WithMessage("ISO code is required.")
            .Length(3)
                .WithMessage("ISO code must be 3 characters long.")
            .Must(BeValidIsoCodeFormat)
                .WithMessage("ISO code must consist of uppercase letters only.");

        RuleFor(x => x.Symbol)
            .NotEmpty()
                .WithMessage("Currency symbol is required.");
    }

    /// <summary>
    /// Ensures the currency is one of the predefined valid currencies.
    /// </summary>
    private bool BeAValidCurrency(Currency currency)
    {
        var predefinedCurrencies = Enumeration.GetAll<Currency>();
        return predefinedCurrencies.Contains(currency);
    }

    /// <summary>
    /// Validates the ISO code format (3 uppercase letters).
    /// </summary>
    private bool BeValidIsoCodeFormat(string isoCode)
    {
        return !string.IsNullOrEmpty(isoCode) && isoCode.All(char.IsUpper);
    }
}