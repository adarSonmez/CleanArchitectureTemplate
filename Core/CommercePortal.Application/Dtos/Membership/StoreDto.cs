namespace CommercePortal.Application.Dtos.Membership;

using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the store data transfer object.
/// </summary>
public record StoreDto
(
    Guid UserId,
    string? Website,
    string? Description,
    ICollection<ProductDto>? Products
) : IDto;