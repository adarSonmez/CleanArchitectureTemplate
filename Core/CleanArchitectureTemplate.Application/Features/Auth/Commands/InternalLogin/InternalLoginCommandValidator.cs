using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;

/// <summary>
/// Validator for the <see cref="RegisterUserCommandRequest"/> class.
/// </summary>
public class InternalLoginCommandValidator : AbstractValidator<InternalLoginCommandRequest>
{
    public InternalLoginCommandValidator()
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