using CleanArchitectureTemplate.Application.Abstractions.Messaging;
using CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;
using CleanArchitectureTemplate.Messaging.Factory.RabbitMq;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CleanArchitectureTemplate.Messaging.Services.RabbitMq;

/// <summary>
/// RabbitMQ-based implementation of the IMessagePublisher interface using asynchronous operations.
/// </summary>
public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly ILogger<RabbitMqMessagePublisher> _logger;
    private readonly RabbitMqConnectionFactory _connectionFactory;

    public RabbitMqMessagePublisher(ILogger<RabbitMqMessagePublisher> logger, RabbitMqConnectionFactory connectionFactory)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc/>
    public async Task PublishAsync<T>(PublishMessageDto<T> messageDto) where T : class
    {
        ArgumentNullException.ThrowIfNull(messageDto.Message);

        var connection = await _connectionFactory.GetConnectionAsync();
        using var channel = await connection.CreateChannelAsync(cancellationToken: messageDto.CancellationToken);

        await channel.ExchangeDeclareAsync(
            exchange: messageDto.ExchangeDeclaration.ExchangeName,
            type: messageDto.ExchangeDeclaration.ExchangeType,
            durable: messageDto.ExchangeDeclaration.Durable,
            autoDelete: messageDto.ExchangeDeclaration.AutoDelete,
            arguments: messageDto.ExchangeDeclaration.Arguments,
            cancellationToken: messageDto.CancellationToken
        );

        var basicProperties = new BasicProperties
        {
            Persistent = messageDto.PersistentMessages,
        };

        if (messageDto.MessageProperties != null)
        {
            basicProperties.ContentType = messageDto.MessageProperties.ContentType;
            basicProperties.Headers = messageDto.MessageProperties.Headers;
            basicProperties.Expiration = messageDto.MessageProperties.Expiration;
            basicProperties.Priority = messageDto.MessageProperties.Priority;
        }

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageDto.Message));
        var exchangeName = messageDto.ExchangeDeclaration?.ExchangeName;

        await channel.BasicPublishAsync(
            exchange: exchangeName ?? string.Empty,
            routingKey: messageDto.RoutingKey ?? string.Empty,
            mandatory: messageDto.Mandatory,
            basicProperties: basicProperties,
            body: body,
            cancellationToken: messageDto.CancellationToken
        );

        _logger.LogInformation("Message published to RabbitMQ: {Message}", messageDto.Message);
    }
}