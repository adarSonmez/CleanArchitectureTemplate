using CleanArchitectureTemplate.Application.Abstractions.Messaging;
using CleanArchitectureTemplate.Messaging.Factory.RabbitMq;
using CleanArchitectureTemplate.Messaging.Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Messaging;

/// <summary>
/// Registers the services for the messaging project.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the messaging services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void AddMessagingServices(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqConnectionFactory>();
        services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();
        services.AddSingleton<IMessageSubscriber, RabbitMqMessageSubscriber>();
    }
}