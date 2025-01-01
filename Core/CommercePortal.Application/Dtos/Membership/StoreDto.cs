namespace CommercePortal.Application.Dtos.Membership;

using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the store data transfer object.
/// </summary>
/// <param name="UserId">The user id of the store.</param>
/// <param name="Website">The website of the store.</param>
/// <param name="Description">The description of the store.</param>
/// <param name="Products">The products that the store has.</param>
public record StoreDto
(
    Guid UserId,
    string? Website,
    string? Description,
    ICollection<ProductDto>? Products
) : IDto;