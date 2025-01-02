using CommercePortal.Application.Features.ProductImageFiles.Commands.UploadProductImages;
using FluentValidation;

namespace CommercePortal.Application.Validators.FluentValidation.ProductImageFiles;

/// <summary>
/// Validator for the <see cref="UploadProductImagesCommandRequest"/> class.
/// </summary>
public class UploadProductImagesValidator : AbstractValidator<UploadProductImagesCommandRequest>
{
    public UploadProductImagesValidator()
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