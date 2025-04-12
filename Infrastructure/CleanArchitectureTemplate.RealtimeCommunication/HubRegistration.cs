using CleanArchitectureTemplate.RealtimeCommunication.Hubs.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitectureTemplate.RealtimeCommunication;

/// <summary>
/// Provides extension methods for registering SignalR hubs.
/// </summary>
public static class HubRegistration
{
    /// <summary>
    /// Maps the SignalR hubs to their respective endpoints.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    public static void MapHubs(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<SignalRProductHub>("/product-hub");
        endpoints.MapHub<SignalROrderHub>("/order-hub");
    }
}