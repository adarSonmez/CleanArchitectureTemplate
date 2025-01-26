using CleanArchitectureTemplate.Domain.ValueObjects;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Common.Validators;

/// <summary>
/// Validator for the <see cref="Address"/> value object.
/// </summary>
public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.PostalCode)
            .NotEmpty()
                .WithMessage("Postal code is required.")
            .MaximumLength(20)
                .WithMessage("Postal code must not exceed 20 characters.");

        RuleFor(x => x.City)
            .NotEmpty()
                .WithMessage("City is required.")
            .MaximumLength(50)
                .WithMessage("City must not exceed 50 characters.");

        RuleFor(x => x.Country)
            .NotNull()
                .WithMessage("Country is required.")
            .SetValidator(new CountryValidator());
    }
}