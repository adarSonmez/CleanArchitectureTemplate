using CleanArchitectureTemplate.Application.Common.Validators;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;

/// <summary>
/// Validator for the <see cref="CreateOrderFromBasketCommandRequest"/> class.
/// </summary>
public class CreateOrderFromBasketCommandRequestValidator : AbstractValidator<CreateOrderFromBasketCommandRequest>
{
    public CreateOrderFromBasketCommandRequestValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotNull()
                .WithMessage("Shipping address is required.")
            .SetValidator(new AddressValidator());
    }
}