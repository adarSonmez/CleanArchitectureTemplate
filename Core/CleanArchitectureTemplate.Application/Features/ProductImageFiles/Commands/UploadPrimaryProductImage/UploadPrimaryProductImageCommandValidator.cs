using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadPrimaryProductImage;

/// <summary>
/// Validator for the <see cref="UploadPrimaryProductImageCommandRequest"/> class.
/// </summary>
public class UploadPrimaryProductImageCommandValidator : AbstractValidator<UploadPrimaryProductImageCommandRequest>
{
    public UploadPrimaryProductImageCommandValidator()
    {
        RuleFor(x => x.Folder)
            .NotEmpty()
                .WithMessage("Folder is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage("ProductId is required.");

        RuleFor(x => x.File)
            .NotNull()
                .WithMessage("File is required.");
    }
}