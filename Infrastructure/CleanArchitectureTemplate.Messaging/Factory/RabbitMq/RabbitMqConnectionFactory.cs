using CleanArchitectureTemplate.Application.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CleanArchitectureTemplate.Messaging.Factory.RabbitMq;

/// <summary>
/// Factory class for creating and reusing an asynchronous RabbitMQ connection.
/// </summary>
public class RabbitMqConnectionFactory
{
    private readonly RabbitMqOptions _options;
    private IConnection _connection = default!;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    public RabbitMqConnectionFactory(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Returns a singleton open connection to RabbitMQ.
    /// </summary>
    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            await _connectionLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = _options.HostName,
                        Port = _options.Port,
                        UserName = _options.UserName,
                        Password = _options.Password,
                        VirtualHost = _options.VirtualHost
                    };

                    _connection = await factory.CreateConnectionAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        return _connection;
    }
}