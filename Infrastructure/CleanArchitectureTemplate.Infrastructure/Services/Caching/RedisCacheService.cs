using CleanArchitectureTemplate.Application.Abstractions.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace CleanArchitectureTemplate.Infrastructure.Services.Caching;

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
    public async Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
    {
        DateTime? absoluteExpirationTime = null;
        if (absoluteExpiration.HasValue)
        {
            absoluteExpirationTime = DateTime.UtcNow.Add(absoluteExpiration.Value);
        }

        var cacheItem = new CacheItem<T>
        {
            Value = value,
            AbsoluteExpiration = absoluteExpirationTime,
            SlidingExpiration = slidingExpiration
        };

        var serializedItem = JsonSerializer.Serialize(cacheItem);

        TimeSpan? expiry = null;
        if (absoluteExpiration.HasValue && slidingExpiration.HasValue)
        {
            expiry = slidingExpiration.Value < absoluteExpiration.Value
                ? slidingExpiration
                : absoluteExpiration;
        }
        else if (absoluteExpiration.HasValue)
        {
            expiry = absoluteExpiration;
        }
        else if (slidingExpiration.HasValue)
        {
            expiry = slidingExpiration;
        }

        await _db.StringSetAsync(key, serializedItem, expiry);
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key)
    {
        var serializedItem = await _db.StringGetAsync(key);
        if (serializedItem.IsNullOrEmpty)
        {
            return default;
        }

        var cacheItem = JsonSerializer.Deserialize<CacheItem<T>>(serializedItem!);
        if (cacheItem == null)
        {
            return default;
        }

        // Check if absolute expiration has passed
        if (cacheItem.AbsoluteExpiration.HasValue && cacheItem.AbsoluteExpiration.Value < DateTime.UtcNow)
        {
            await _db.KeyDeleteAsync(key);
            return default;
        }

        // Reset the sliding expiration if applicable
        if (cacheItem.SlidingExpiration.HasValue)
        {
            await _db.KeyExpireAsync(key, cacheItem.SlidingExpiration);
        }

        return cacheItem.Value;
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
    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
    {
        var cachedValue = await GetAsync<T>(key);
        if (cachedValue != null)
        {
            return cachedValue;
        }

        var result = await acquire();
        await SetAsync(key, result, slidingExpiration, absoluteExpiration);
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
        var hashValue = await _db.HashGetAsync(key, field);
        return hashValue.IsNullOrEmpty ? null : hashValue.ToString();
    }

    /// <inheritdoc/>
    public async Task<bool> HashRemoveAsync(string key, string field)
    {
        return await _db.HashDeleteAsync(key, field);
    }
}