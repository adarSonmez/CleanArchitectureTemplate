namespace CommercePortal.Application.Features.Commands.ProductImageFiles.UploadProductImage;

/// <summary>
/// Represents the response of the <see cref="UploadProductImageCommandRequest"/>.
/// </summary>
/// <param name="Id">Product Image Id</param>
/// <param name="Folder">Folder name</param>
/// <param name="File">File name</param>
public record UploadProductImageCommandResponse
(
    Guid Id,
    string Folder,
    string File
);