using CommercePortal.Application.Common.Validators;
using FluentValidation;

namespace CommercePortal.Application.Features.Products.Commands.CreateProduct;

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

        RuleFor(x => x.StandardPrice)
            .NotNull()
                .WithMessage("Standard price is required.")
            .SetValidator(new MoneyValidator());

        RuleFor(x => x.CategoryIds)
            .NotNull()
            .Must(x => x.Count > 0)
                .WithMessage("At least one category is required.");
    }
}