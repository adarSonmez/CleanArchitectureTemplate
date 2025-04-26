using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.UpdateUser;

/// <summary>
/// Validator for the <see cref="UpdateUserCommandRequest"/> class.
/// </summary>
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("User ID is required.");

        RuleFor(x => x.FullName)
            .MaximumLength(100)
            .MinimumLength(3)
                .WithMessage("Full name must be between 3 and 100 characters.");

        RuleFor(x => x.Email)
            .EmailAddress()
                .WithMessage("Email is not valid.");

        RuleFor(x => x.UserName)
            .MinimumLength(1)
                .WithMessage("Username cannot be empty.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+\d{10,15}$")
            .WithMessage("Phone number is not valid. It should include the country code and contain only digits without spaces.");
    }
}