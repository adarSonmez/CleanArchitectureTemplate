using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.BasketItems.Commands.RemoveBasketItemFromBasket;

/// <summary>
/// Validator for the <see cref="RemoveBasketItemFromBasketCommandRequest"/> class.
/// </summary>
public class RemoveBasketItemFromBasketCommandValidator : AbstractValidator<RemoveBasketItemFromBasketCommandRequest>
{
    public RemoveBasketItemFromBasketCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
                .When(x => !x.Clear)
                    .WithMessage("Quantity must be greater than 0.");
    }
}