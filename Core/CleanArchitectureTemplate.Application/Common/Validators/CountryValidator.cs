using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="Country"/> smart enum.
/// </summary>
public class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator()
    {
        RuleFor(x => x)
            .NotNull()
                .WithMessage("Country is required.")
            .Must(BeAValidCountry)
                .WithMessage("The provided country is not valid.");

        RuleFor(x => x.IsoAlpha2)
            .NotEmpty()
                .WithMessage("ISO Alpha-2 code is required.")
            .Length(2)
                .WithMessage("ISO Alpha-2 code must be 2 characters long.");

        RuleFor(x => x.IsoAlpha3)
            .NotEmpty()
                .WithMessage("ISO Alpha-3 code is required.")
            .Length(3)
                .WithMessage("ISO Alpha-3 code must be 3 characters long.");

        RuleFor(x => x.NumericCode)
            .GreaterThan(0)
                .WithMessage("Numeric code must be greater than zero.");
    }

    /// <summary>
    /// Ensures the country is a predefined valid country.
    /// </summary>
    private bool BeAValidCountry(Country country)
    {
        var predefinedCountries = Enumeration.GetAll<Country>();
        return predefinedCountries.Contains(country);
    }
}