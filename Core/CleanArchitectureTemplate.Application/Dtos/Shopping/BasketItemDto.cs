using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Shopping;

/// <summary>
/// Represents data transfer object for <see cref="BasketItem"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="BasketId">The basket that the item belongs to.</param>
/// <param name="ProductId">The product that the item represents.</param>
/// <param name="Quantity">The quantity of the product in the basket.</param>
public record BasketItemDto
(
    Guid Id = default,
    Guid BasketId = default,
    Guid ProductId = default,
    int Quantity = default
) : IDto;