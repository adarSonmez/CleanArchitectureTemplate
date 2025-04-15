namespace CleanArchitectureTemplate.Application.Options;

/// <summary>
/// Options used to configure Redis connection and Sentinel settings.
/// </summary>
public class RedisOptions
{
    /// <summary>
    /// Gets or sets the list of Sentinel endpoints (e.g., "localhost:6383").
    /// </summary>
    public required List<string> SentinelEndpoints { get; set; }

    /// <summary>
    /// Gets or sets the master name used for Sentinel lookup.
    /// </summary>
    public required string MasterName { get; set; }

    /// <summary>
    /// Gets or sets the master mapping for Sentinel lookup.
    /// </summary>
    public required List<List<string>> MasterMapping { get; set; }

    /// <summary>
    /// Gets or sets the timeout for synchronous operations.
    /// </summary>
    public int SyncTimeout { get; set; } = 5000;

    /// <summary>
    /// Gets or sets the connection timeout.
    /// </summary>
    public int ConnectTimeout { get; set; } = 5000;
}