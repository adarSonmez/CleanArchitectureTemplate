using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.ProductImageFiles.Commands.UploadSecondaryProductImages;

/// <summary>
/// Validator for the <see cref="UploadSecondaryProductImagesCommandRequest"/> class.
/// </summary>
public class UploadSecondaryProductImagesCommandValidator : AbstractValidator<UploadSecondaryProductImagesCommandRequest>
{
    public UploadSecondaryProductImagesCommandValidator()
    {
        RuleFor(x => x.Folder)
            .NotEmpty()
                .WithMessage("Folder is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage("ProductId is required.");

        RuleFor(x => x.Files)
            .NotEmpty()
                .WithMessage("Files are required.")
            .Must(x => x.Count <= 8)
                .WithMessage("Maximum 8 files are allowed.");
    }
}