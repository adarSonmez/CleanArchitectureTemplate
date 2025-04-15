using CleanArchitectureTemplate.Application.Behaviours;
using CleanArchitectureTemplate.Application.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Application;

/// <summary>
/// Registers the services for the application project.
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

        #region Options

        services.Configure<JwtOptions>(configuration.GetSection("JWT"));
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMQ"));
        services.Configure<RedisOptions>(configuration.GetSection("Redis"));
        services.Configure<SmtpOptions>(configuration.GetSection("SMTP"));

        #endregion Options
    }
}