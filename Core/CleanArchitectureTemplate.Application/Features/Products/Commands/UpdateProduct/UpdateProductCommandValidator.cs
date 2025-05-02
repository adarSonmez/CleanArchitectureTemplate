using CleanArchitectureTemplate.Application.Common.Validators;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Validator for the <see cref="UpdateProductCommandRequest"/> class.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .MinimumLength(3)
                .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0.");

        When(x => x.StandardPrice != null, () =>
        {
            RuleFor(x => x.StandardPrice!.CurrencyIsoCode)
                .NotEmpty()
                    .WithMessage("Currency ISO code is required.")
                .Must(code =>
                {
                    try
                    {
                        _ = Currency.FromIsoCode(code);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage("Invalid currency ISO code.");

            RuleFor(x => x.StandardPrice!.Amount)
                .GreaterThan(0)
                    .WithMessage("Standard price must be greater than 0.");
        });

        RuleFor(x => x.CategoryIds)
            .NotEmpty()
                .WithMessage("At least one category is required.")
            .When(x => x.CategoryIds is not null);
    }
}