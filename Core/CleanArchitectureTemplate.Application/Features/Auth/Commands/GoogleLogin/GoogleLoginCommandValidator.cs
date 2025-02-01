using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;

/// <summary>
/// Validator for the <see cref="GoogleLoginCommandRequest"/> class.
/// </summary>
public class GoogleLoginCommandValidator : AbstractValidator<GoogleLoginCommandRequest>
{
    public GoogleLoginCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("User ID is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithMessage("Email is not valid.");

        RuleFor(x => x.IdToken)
            .NotEmpty()
                .WithMessage("ID token is required.");

        RuleFor(x => x.Provider)
            .NotEmpty()
                .WithMessage("Provider is required.");

        RuleFor(x => x.FirstName)
            .MaximumLength(50)
                .WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(50)
                .WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.PhotoUrl)
            .Must(url => string.IsNullOrWhiteSpace(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Photo URL must be a valid URL.");
    }
}