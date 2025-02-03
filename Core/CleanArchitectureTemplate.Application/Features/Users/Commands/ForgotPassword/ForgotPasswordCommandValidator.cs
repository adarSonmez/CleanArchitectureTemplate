using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ForgotPassword;

/// <summary>
/// Validator for the <see cref="ForgotPasswordCommandRequest"/> class.
/// </summary>
public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommandRequest>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithMessage("Email is not valid.");
    }
}