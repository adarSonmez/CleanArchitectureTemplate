using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.RealtimeCommunication.HubServices.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.RealtimeCommunication;

/// <summary>
/// Registers the services for the RealtimeCommunication project.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the realtime communication services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static IServiceCollection AddRealTimeCommunicationServices(this IServiceCollection services)
    {
        services.AddSignalR();

        // Add hub services
        services.AddScoped<IProductHubService, SignalRProductHubService>();
        services.AddScoped<IOrderHubService, SignalROrderHubService>();
        services.AddScoped<IAIHubService, SignalRAIHubService>();

        return services;
    }
}