using CleanArchitectureTemplate.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace CleanArchitectureTemplate.Infrastructure.Services.Redis;

/// <summary>
/// Provides a Redis connection using Sentinel for master discovery.
/// </summary>
public class RedisConnectorService
{
    private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

    /// <summary>
    /// Gets the Redis <see cref="ConnectionMultiplexer"/> instance.
    /// </summary>
    public ConnectionMultiplexer Connection => _lazyConnection.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisConnectorService"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration containing Redis settings.</param>
    public RedisConnectorService(IConfiguration configuration)
    {
        var sentinelSection = configuration.GetSection("Redis:Sentinel");
        var sentinelEndpoints = sentinelSection.GetSection("EndPoints").Get<string[]>()!;
        var masterName = sentinelSection["MasterName"]!;

        var connectionOptions = new ConfigurationOptions
        {
            AbortOnConnectFail = bool.Parse(configuration["Redis:ConnectionOptions:AbortOnConnectFail"] ?? "false"),
            SyncTimeout = int.Parse(configuration["Redis:ConnectionOptions:SyncTimeout"] ?? "5000")
        };

        _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConnectionMultiplexer? sentinelConnection = null;
            foreach (var endpoint in sentinelEndpoints)
            {
                try
                {
                    sentinelConnection = ConnectionMultiplexer.Connect(endpoint);
                    break;
                }
                catch (Exception ex)
                {
                    throw new ServiceUnavailableException(ex.Message, innerException: ex);
                }
            }

            if (sentinelConnection == null)
            {
                throw new ServiceUnavailableException("Could not connect to any Redis Sentinel endpoint.");
            }

            var server = sentinelConnection.GetServer(sentinelEndpoints.First());
            var masterEndpoint = server.SentinelGetMasterAddressByName(masterName)
            ?? throw new ServiceUnavailableException("Sentinel did not return a master endpoint.");
            connectionOptions.EndPoints.Add(masterEndpoint);
            return ConnectionMultiplexer.Connect(connectionOptions);
        });
    }
}