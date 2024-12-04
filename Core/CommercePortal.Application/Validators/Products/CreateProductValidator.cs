using CommercePortal.Application.ViewModels.Products;
using FluentValidation;

namespace CommercePortal.Application.Validators.Products;

/// <summary>
/// Validator for the <see cref="CreateProductVM"/> class.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductVM>
{
    public CreateProductValidator()
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

        RuleFor(x => x.Price)
            .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");
    }
}