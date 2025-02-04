using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.AddBasketItemToBasket;

/// <summary>
/// Validator for the <see cref="AddBasketItemToBasketCommandRequest"/> class.
/// </summary>
public class AddBasketItemToBasketCommandValidator : AbstractValidator<AddBasketItemToBasketCommandRequest>
{
    public AddBasketItemToBasketCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
    }
}