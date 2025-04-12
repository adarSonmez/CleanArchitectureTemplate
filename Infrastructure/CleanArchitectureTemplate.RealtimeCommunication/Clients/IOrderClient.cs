using CleanArchitectureTemplate.Application.Dtos.Ordering;

namespace CleanArchitectureTemplate.RealtimeCommunication.Clients;

/// <summary>
/// Client for managing real-time communication related to orders.
/// </summary>
public interface IOrderClient
{
    /// <summary>
    /// Notifies clients that a order has been created.
    /// </summary>
    Task OrderCreatedAsync(OrderDto message);

    /// <summary>
    /// Notifies clients that a client has connected.
    /// </summary>
    Task ClientConnectedAsync(string connectionId);

    /// <summary>
    /// Notifies clients that a client has disconnected.
    /// </summary>
    Task ClientDisconnectedAsync(string connectionId);
}