using CommercePortal.Application.Dtos.Ordering;
using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Membership;
/// <summary>
/// Represents the customer data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="UserId">The user id of the customer.</param>
/// <param name="Age">The age of the customer.</param>
/// <param name="Gender">The gender of the customer.</param>
/// <param name="Orders">The orders of the customer.</param>
public record CustomerDto
(
    Guid Id,
    Guid UserId,
    short? Age,
    Gender Gender,
    ICollection<OrderDto>? Orders
) : IDto;