using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.RegisterAppUser;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.AppUsers.Commands.LoginAppUser;

/// <summary>
/// Validator for the <see cref="RegisterAppUserCommandRequest"/> class.
/// </summary>
public class LoginAppUserCommandValidator : AbstractValidator<LoginAppUserCommandRequest>
{
    public LoginAppUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Username is required when email is not provided.");

        RuleFor(x => x.Email)
            .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.UserName))
                .WithMessage("Email is required when username is not provided.")
            .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Email is not valid.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password is required.");
    }
}