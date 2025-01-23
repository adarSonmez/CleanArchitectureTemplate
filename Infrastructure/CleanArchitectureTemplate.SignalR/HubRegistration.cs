using CleanArchitectureTemplate.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitectureTemplate.SignalR;

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
        endpoints.MapHub<ProductHub>("/product-hub");
    }
}