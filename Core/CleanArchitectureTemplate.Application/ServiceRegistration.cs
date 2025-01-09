using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Application;

/// <summary>
/// Represents the service registration for the application layer.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        services.AddHttpClient();
    }
}