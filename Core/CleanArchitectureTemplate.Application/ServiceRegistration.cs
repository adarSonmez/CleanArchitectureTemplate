using CleanArchitectureTemplate.Application.Behaviours;
using CleanArchitectureTemplate.Application.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
    /// <param name="configuration">The configuration to use for the services.</param>
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        services.AddHttpClient();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        #region Settings

        services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));

        #endregion Settings
    }
}