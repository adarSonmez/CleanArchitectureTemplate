using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ResetPassword
{
    /// <summary>
    /// Validator for the <see cref="ResetPasswordCommandRequest"/> class.
    /// </summary>
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommandRequest>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Reset token is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                    .WithMessage("New password is required.")
                .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters long.");
        }
    }
}