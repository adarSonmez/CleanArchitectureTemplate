using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.AppUsers.Commands.RegisterAppUser;

/// <summary>
/// Validator for the <see cref="RegisterAppUserCommandRequest"/> class.
/// </summary>
public class RegisterAppUserCommandValidator : AbstractValidator<RegisterAppUserCommandRequest>
{
    public RegisterAppUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(100)
                .MinimumLength(3)
                .WithMessage("Full name must be between 5 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithMessage("Email is not valid.");

        RuleFor(x => x.UserName)
            .NotEmpty()
                .WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password is required.");

        RuleFor(x => x.PasswordConfirmation)
            .NotEmpty()
                .WithMessage("Password confirmation is required.")
            .Equal(x => x.Password)
                .WithMessage("Password confirmation does not match the password.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number is not valid.");
    }
}