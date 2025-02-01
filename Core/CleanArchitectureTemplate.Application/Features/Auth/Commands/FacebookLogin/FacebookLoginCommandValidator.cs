using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;

/// <summary>
/// Validator for the <see cref="FacebookLoginCommandRequest"/> class.
/// </summary>
public class FacebookLoginCommandValidator : AbstractValidator<FacebookLoginCommandRequest>
{
    public FacebookLoginCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
                .WithMessage("Access token is required.");

        RuleFor(x => x.Provider)
            .NotEmpty()
                .WithMessage("Provider is required.");
    }
}