using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Orders.Commands.UpdateOrderStatus;

/// <summary>
/// Validator for the <see cref="UpdateOrderStatusCommandRequest"/> class.
/// </summary>
public class UpdateOrderStatusCommandRequestValidator : AbstractValidator<UpdateOrderStatusCommandRequest>
{
    public UpdateOrderStatusCommandRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Order ID is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
                .WithMessage("Invalid order status.");
    }
}