using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Ordering;

/// <summary>
/// Represents data transfer object for <see cref="Order"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Status">The status of the order.</param>
/// <param name="Basket">The basket associated with the order.</param>
public record OrderDto
(
    Guid Id = default,
    OrderStatus Status = default,
    BasketDto? Basket = default
) : IDto;