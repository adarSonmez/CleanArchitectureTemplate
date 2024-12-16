﻿using CommercePortal.Application.Features.Commands.AppUsers.CreateUser;
using FluentValidation;

namespace CommercePortal.Application.Validators.AppUsers;

/// <summary>
/// Validator for the <see cref="CreateAppUserCommandRequest"/> class.
/// </summary>
public class CreateAppUserValidator : AbstractValidator<CreateAppUserCommandRequest>
{
    public CreateAppUserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
                .WithMessage("Full name is required.");

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
            .NotEmpty()
                .WithMessage("Phone number is required.");
    }
}