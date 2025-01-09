using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.LoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.RegisterAppUser;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Validators.FluentValidation.AppUsers;

/// <summary>
/// Validator for the <see cref="RegisterAppUserCommandRequest"/> class.
/// </summary>
public class LoginAppUserValidator : AbstractValidator<LoginAppUserCommandRequest>
{
    public LoginAppUserValidator()
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