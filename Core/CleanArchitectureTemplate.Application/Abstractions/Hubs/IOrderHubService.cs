using CleanArchitectureTemplate.Application.Dtos.Ordering;

namespace CleanArchitectureTemplate.Application.Abstractions.Hubs;

/// <summary>
/// Interface for the order hub service to manage real-time order-related operations.
/// </summary>
public interface IOrderHubService
{
    /// <summary>
    /// Sends a order created event to the clients.
    /// </summary>
    /// <param name="orderDto">The order DTO to send.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task SendOrderCreatedAsync(OrderDto orderDto);
}