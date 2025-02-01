using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Auth.Queries.ValidateToken;

/// <summary>
/// Validator for the <see cref="ValidateTokenQueryRequest"/> class.
/// Ensures that the token is not empty or null.
/// </summary>
public class ValidateTokenQueryValidator : AbstractValidator<ValidateTokenQueryRequest>
{
    public ValidateTokenQueryValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
                .WithMessage("Token is required.");
    }
}