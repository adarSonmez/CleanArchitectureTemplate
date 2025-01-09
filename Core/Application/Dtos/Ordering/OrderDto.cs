﻿using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using CleanArchitectureTemplate.Domain.ValueObjects;

namespace CleanArchitectureTemplate.Application.Dtos.Ordering;

/// <summary>
/// Represents data transfer object for <see cref="Order"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Status">The status of the order.</param>
/// <param name="TotalAmount">The total amount of the order.</param>
/// <param name="ShippingAddress">The shipping address of the order.</param>
/// <param name="CustomerId">The Id of the customer who placed the order.</param>
/// <param name="OrderItems">The order items of the order.</param>
public record OrderDto
(
    Guid Id,
    OrderStatus Status,
    Money? TotalAmount,
    Address ShippingAddress,
    Guid CustomerId,
    ICollection<OrderItemDto>? OrderItems
) : IDto;