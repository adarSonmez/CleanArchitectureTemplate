using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace CleanArchitectureTemplate.Infrastructure.Services.Caching;

/// <summary>
/// Provides a Redis connection using Sentinel for master discovery.
/// </summary>
public sealed class RedisConnectorService : IDisposable
{
    private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
    private readonly RedisSettings _settings;

    /// <summary>
    /// Gets the Redis ConnectionMultiplexer instance.
    /// </summary>
    public ConnectionMultiplexer Connection => _lazyConnection.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisConnectorService"/> class using provided options.
    /// </summary>
    /// <param name="options">The Redis settings options.</param>
    /// <exception cref="ArgumentNullException">Thrown if options are null.</exception>
    public RedisConnectorService(IOptions<RedisSettings> options)
    {
        _settings = options?.Value
            ?? throw new ArgumentNullException(nameof(options));

        _lazyConnection = new Lazy<ConnectionMultiplexer>(CreateConnection, isThreadSafe: true);
    }

    private ConnectionMultiplexer CreateConnection()
    {
        // Build configuration for connecting to the Sentinel(s)
        var sentinelOptions = new ConfigurationOptions
        {
            CommandMap = CommandMap.Sentinel,
            TieBreaker = "",
            AllowAdmin = true,
            SyncTimeout = _settings.SyncTimeout,
            ConnectTimeout = _settings.ConnectTimeout,
            AbortOnConnectFail = false,
        };

        foreach (var endpoint in _settings.SentinelEndpoints)
        {
            sentinelOptions.EndPoints.Add(endpoint);
        }

        ConnectionMultiplexer sentinelConnection;
        try
        {
            sentinelConnection = ConnectionMultiplexer.Connect(sentinelOptions);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Unable to connect to any Sentinel endpoint.", innerException: ex);
        }

        var sentinelEndpoints = sentinelConnection.GetEndPoints();
        if (sentinelEndpoints.Length == 0)
        {
            throw new ServiceUnavailableException("No Sentinel endpoint is reachable.");
        }

        // Use the first reachable Sentinel to get the master endpoint
        var masterEndpoint = sentinelConnection
            .GetServer(sentinelEndpoints.First())
            .SentinelGetMasterAddressByName(_settings.MasterName)
                ?? throw new ServiceUnavailableException($"No master endpoint found for master name {_settings.MasterName}.");

        // Map the discovered master endpoint to a local endpoint using the provided mapping
        var masterEndpointKey = masterEndpoint.ToString()!;
        var masterMappingDict = _settings.MasterMapping.ToDictionary(list => list[0], list => list[1]);
        string? mappedEndpoint = null;

        if (masterMappingDict.Keys.FirstOrDefault() == masterMappingDict.Values.FirstOrDefault()) // Running from docker-compose
        {
            mappedEndpoint = masterEndpointKey;
        }
        else if (!masterMappingDict.TryGetValue(masterEndpointKey, out mappedEndpoint))
        {
            throw new NotImplementedException($"Mapping for master endpoint {masterEndpointKey} is not configured.");
        }

        var masterOptions = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            SyncTimeout = _settings.SyncTimeout,
            ConnectTimeout = _settings.ConnectTimeout,
        };

        masterOptions.EndPoints.Add(mappedEndpoint);

        return ConnectionMultiplexer.Connect(masterOptions);
    }

    /// <summary>
    /// Disposes the Redis connection if it was created.
    /// </summary>
    public void Dispose()
    {
        if (_lazyConnection.IsValueCreated)
        {
            Connection.Dispose();
        }
    }
}