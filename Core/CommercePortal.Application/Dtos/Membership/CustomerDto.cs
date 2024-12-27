using CommercePortal.Application.Dtos.Ordering;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Membership;
/// <summary>
/// Represents the customer data transfer object.
/// </summary>
public record CustomerDto
(
    Guid Id,
    Guid UserId,
    short? Age,
    Gender Gender,
    ICollection<OrderDto>? Orders
) : IDto;