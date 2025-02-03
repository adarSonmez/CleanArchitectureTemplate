using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ChangePassword;

/// <summary>
/// Validator for the <see cref="ChangePasswordCommandRequest"/> class.
/// </summary>
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommandRequest>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
                .WithMessage("User ID is required.");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
                .WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
                .WithMessage("New password is required.")
            .MinimumLength(6)
                .WithMessage("New password must be at least 6 characters long.");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
                .WithMessage("Password confirmation is required.")
            .Equal(x => x.NewPassword)
                .WithMessage("New password and confirmation password do not match.");
    }
}