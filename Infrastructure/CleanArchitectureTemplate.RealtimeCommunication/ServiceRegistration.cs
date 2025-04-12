using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.RealtimeCommunication.HubServices.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.RealtimeCommunication;

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
        services.AddScoped<IProductHubService, SignalRProductHubService>();
        services.AddScoped<IOrderHubService, SignalROrderHubService>();
        services.AddScoped<IAIHubService, SignalRAIHubService>();
    }
}