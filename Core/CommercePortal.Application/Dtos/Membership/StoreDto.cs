using CommercePortal.Application.Dtos.Marketing;
using CommercePortal.Domain.Entities.Membership;
using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Membership;

/// <summary>
/// Represents data transfer object for <see cref="Store"/>
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