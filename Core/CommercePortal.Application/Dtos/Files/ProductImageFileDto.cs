namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the product image file data transfer object.
/// </summary>
public record ProductImageFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails,
    ICollection<ProductDto>? Products
) : IDto;