using CleanArchitectureTemplate.Application.Common.Validators;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Validator for the <see cref="CreateProductCommandRequest"/> class.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name is required.")
            .MaximumLength(100)
            .MinimumLength(3)
                .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0.");

        RuleFor(x => x.DiscountRate)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1)
                .WithMessage("Discount rate must be between 0 and 1.");

        RuleFor(x => x.StandardPrice.CurrencyIsoCode)
            .NotEmpty()
                .WithMessage("Currency ISO code is required.")
            .Must(x =>
            {
                try
                {
                    _ = Currency.FromIsoCode(x);
                    return true;
                }
                catch
                {
                    return false;
                }
            })
            .WithMessage("Invalid currency ISO code.");

        RuleFor(x => x.StandardPrice.Amount)
            .GreaterThan(0)
                .WithMessage("Standard price must be greater than 0.");

        RuleFor(x => x.CategoryIds)
            .NotNull()
            .Must(x => x.Count > 0)
                .WithMessage("At least one category is required.");
    }
}