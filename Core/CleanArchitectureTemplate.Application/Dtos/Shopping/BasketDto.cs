using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Application.Dtos.Shopping;

/// <summary>
/// Represents data transfer object for <see cref="Basket"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="CustomerId">The customer that the basket belongs to.</param>
/// <param name="TotalAmount">The total amount of the basket.</param>
/// <param name="Ordered">Whether the basket has been ordered.</param>
/// <param name="BasketItems">The items in the basket.</param>
public record BasketDto
(
    Guid Id = default,
    Guid CustomerId = default,
    Money? TotalAmount = default,
    bool Ordered = default,
    ICollection<BasketItemDto>? BasketItems = default
) : IDto;