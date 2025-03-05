using CleanArchitectureTemplate.Application.Abstractions.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace CleanArchitectureTemplate.Infrastructure.Services.Redis;

/// <summary>
/// Implements <see cref="ICacheService"/> using Redis.
/// </summary>
public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(RedisConnectorService redisConnectorService)
    {
        _db = redisConnectorService.Connection.GetDatabase();
    }

    /// <inheritdoc/>
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, expiration);
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _db.StringGetAsync(key);
        if (json.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(json!);
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(string key)
    {
        return await _db.KeyExistsAsync(key);
    }

    /// <inheritdoc/>
    public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiration)
    {
        return await _db.KeyExpireAsync(key, expiration);
    }

    /// <inheritdoc/>
    public async Task<long> IncrementAsync(string key, long value = 1)
    {
        return await _db.StringIncrementAsync(key, value);
    }

    /// <inheritdoc/>
    public async Task<long> DecrementAsync(string key, long value = 1)
    {
        return await _db.StringDecrementAsync(key, value);
    }

    /// <inheritdoc/>
    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? expiration = null)
    {
        var cachedValue = await GetAsync<T>(key);
        if (cachedValue != null)
        {
            return cachedValue;
        }
        T result = await acquire();
        await SetAsync(key, result, expiration);
        return result;
    }

    /// <inheritdoc/>
    public async Task<bool> HashSetAsync(string key, string field, string value)
    {
        return await _db.HashSetAsync(key, field, value);
    }

    /// <inheritdoc/>
    public async Task<string?> HashGetAsync(string key, string field)
    {
        var value = await _db.HashGetAsync(key, field);
        return value.IsNullOrEmpty ? null : value.ToString();
    }

    /// <inheritdoc/>
    public async Task<bool> HashRemoveAsync(string key, string field)
    {
        return await _db.HashDeleteAsync(key, field);
    }
}