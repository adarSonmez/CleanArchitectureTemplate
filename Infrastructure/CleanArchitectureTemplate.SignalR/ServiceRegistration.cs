using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.SignalR;

/// <summary>
/// Represents the service registration for the SignalR project.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR();

        // Add hub services
        services.AddScoped<IProductHubService, ProductHubService>();
        services.AddScoped<IOrderHubService, OrderHubService>();
        services.AddScoped<IAIHubService, AIHubService>();
    }
}