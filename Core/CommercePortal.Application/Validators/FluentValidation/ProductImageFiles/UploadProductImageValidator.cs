using CommercePortal.Application.Features.Commands.ProductImageFiles.UploadProductImage;
using FluentValidation;

namespace CommercePortal.Application.Validators.FluentValidation.ProductImageFiles;

/// <summary>
/// Validator for the <see cref="UploadProductImageCommandRequest"/> class.
/// </summary>
public class UploadProductImageValidator : AbstractValidator<UploadProductImageCommandRequest>
{
    public UploadProductImageValidator()
    {
        RuleFor(x => x.Folder)
            .NotEmpty()
                .WithMessage("Folder is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage("ProductId is required.");

        RuleFor(x => x.File)
            .NotEmpty()
                .WithMessage("File is required.");
    }
}