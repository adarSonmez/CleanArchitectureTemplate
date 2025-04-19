using CleanArchitectureTemplate.Application.Abstractions.Messaging;
using CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;
using CleanArchitectureTemplate.Messaging.Factory.RabbitMq;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace CleanArchitectureTemplate.Messaging.Services.RabbitMq;

/// <summary>
/// RabbitMQ-based implementation of the IMessageSubscriber interface using asynchronous operations.
/// </summary>
public class RabbitMqMessageSubscriber : IMessageSubscriber
{
    private readonly ILogger<RabbitMqMessageSubscriber> _logger;
    private readonly RabbitMqConnectionFactory _connectionFactory;
    public IDictionary<string, IChannel> ActitveChannels { get; } = new ConcurrentDictionary<string, IChannel>();

    public RabbitMqMessageSubscriber(ILogger<RabbitMqMessageSubscriber> logger,
                                     RabbitMqConnectionFactory connectionFactory)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc/>
    public async Task<string> SubscribeAsync<T>(SubscribeMessageDto<T> messageDto) where T : class
    {
        var connection = await _connectionFactory.GetConnectionAsync();
        var channel = await connection.CreateChannelAsync(cancellationToken: messageDto.CancellationToken);

        await channel.ExchangeDeclareAsync(
            exchange: messageDto.ExchangeDeclaration.ExchangeName,
            type: messageDto.ExchangeDeclaration.ExchangeType,
            durable: messageDto.ExchangeDeclaration.Durable,
            autoDelete: messageDto.ExchangeDeclaration.AutoDelete,
            arguments: messageDto.ExchangeDeclaration.Arguments,
            cancellationToken: messageDto.CancellationToken
        );

        var queueName = string.Empty;

        if (messageDto.QueueDeclaration != null)
        {
            queueName = messageDto.QueueDeclaration.QueueName;
            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: messageDto.QueueDeclaration.Durable,
                exclusive: messageDto.QueueDeclaration.Exclusive,
                autoDelete: messageDto.QueueDeclaration.AutoDelete,
                arguments: messageDto.QueueDeclaration.Arguments,
                cancellationToken: messageDto.CancellationToken
            );
        }
        else
        {
            queueName = await channel.QueueDeclareAsync();
        }

        await channel.QueueBindAsync(
            queue: queueName,
            exchange: messageDto.ExchangeDeclaration!.ExchangeName,
            routingKey: messageDto.RoutingKey ?? string.Empty,
            cancellationToken: messageDto.CancellationToken
        );

        if (messageDto.FairDispatch)
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var deserializedMessage = JsonSerializer.Deserialize<T>(message);
            if (deserializedMessage != null)
            {
                try
                {
                    await messageDto.OnMessage(deserializedMessage);
                    _logger.LogInformation("Message processed from queue '{QueueName}'. Message body: {MessageBody}", queueName, message);
                }
                catch
                {
                    if (!messageDto.AutoAck)
                    {
                        await channel.BasicNackAsync(
                            deliveryTag: ea.DeliveryTag,
                            multiple: messageDto.Multiple,
                            requeue: messageDto.Requeue
                        );
                        _logger.LogError("Message processing failed from queue '{QueueName}'. Message body: {MessageBody}", queueName, message);
                    }
                    throw;
                }
            }

            if (!messageDto.AutoAck)
            {
                await channel.BasicAckAsync(
                    deliveryTag: ea.DeliveryTag,
                    multiple: messageDto.Multiple
                );
                _logger.LogInformation("Message acknowledged from queue '{QueueName}'. Message body: {MessageBody}", queueName, message);
            }
        };

        var consumerTag = await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: messageDto.AutoAck,
            consumer: consumer,
            cancellationToken: messageDto.CancellationToken
        );

        _logger.LogInformation("Consumer with tag '{ConsumerTag}' subscribed to queue '{QueueName}'.", consumerTag, queueName);

        ActitveChannels.TryAdd(consumerTag, channel);

        return consumerTag;
    }

    /// <inheritdoc/>
    public async Task UnsubscribeAsync(string consumerTag, CancellationToken cancellationToken = default)
    {
        var connection = await _connectionFactory.GetConnectionAsync();
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        await channel.BasicCancelAsync(consumerTag, cancellationToken: cancellationToken);

        if (ActitveChannels.Remove(consumerTag, out var channelToDispose))
        {
            channelToDispose.Dispose();
        }

        _logger.LogInformation("Consumer with tag '{ConsumerTag}' unsubscribed.", consumerTag);
    }
}