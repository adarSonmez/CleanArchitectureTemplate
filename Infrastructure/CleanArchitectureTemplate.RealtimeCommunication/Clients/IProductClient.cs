using CleanArchitectureTemplate.Application.Dtos.Shopping;

namespace CleanArchitectureTemplate.RealtimeCommunication.Clients;

/// <summary>
/// Client for managing real-time communication related to products.
/// </summary>
public interface IProductClient
{
    /// <summary>
    /// Notifies clients that a product has been added.
    /// </summary>
    Task ProductAddedAsync(ProductDto message);

    /// <summary>
    /// Notifies clients that a client has connected.
    /// </summary>
    Task ClientConnectedAsync(string connectionId);

    /// <summary>
    /// Notifies clients that a client has disconnected.
    /// </summary>
    Task ClientDisconnectedAsync(string connectionId);
}