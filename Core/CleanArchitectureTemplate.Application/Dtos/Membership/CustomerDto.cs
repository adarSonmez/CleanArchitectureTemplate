using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Membership;

/// <summary>
/// Represents data transfer object for <see cref="Customer"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="UserId">The user id of the customer.</param>
/// <param name="Age">The age of the customer.</param>
/// <param name="Gender">The gender of the customer.</param>
/// <param name="Orders">The orders of the customer.</param>
public record CustomerDto
(
    Guid Id = default!,
    Guid UserId = default!,
    short Age = 0,
    Gender Gender = default!,
    ICollection<OrderDto>? Orders = null
) : IDto;