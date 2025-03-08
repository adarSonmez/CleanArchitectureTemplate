namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Defines a contract for a caching service abstraction.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Stores a value in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="key">The key under which the value will be stored.</param>
    /// <param name="value">The value to store.</param>
    /// <param name="slidingExpiration">Optional sliding expiration timespan for the cached value.</param>
    /// <param name="absoluteExpiration">Optional absolute expiration timespan for the cached value.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);

    /// <summary>
    /// Retrieves a value from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="key">The key under which the value is stored.</param>
    /// <returns>A task that represents the asynchronous operation, containing the value if found; otherwise, default.</returns>
    Task<T?> GetAsync<T>(string key);

    /// <summary>
    /// Removes a value from the cache.
    /// </summary>
    /// <param name="key">The key of the cached value to remove.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveAsync(string key);

    /// <summary>
    /// Determines whether the specified key exists in the cache.
    /// </summary>
    /// <param name="key">The key to check for existence.</param>
    /// <returns>A task that represents the asynchronous operation, containing true if the key exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// Sets the expiration time for a key.
    /// </summary>
    /// <param name="key">The key for which to set expiration.</param>
    /// <param name="expiration">The expiration timespan.</param>
    /// <returns>A task that represents the asynchronous operation, containing true if the timeout was set successfully; otherwise, false.</returns>
    Task<bool> KeyExpireAsync(string key, TimeSpan? expiration);

    /// <summary>
    /// Increments the numeric value stored at the specified key by the given amount.
    /// </summary>
    /// <param name="key">The key of the numeric value.</param>
    /// <param name="value">The amount by which to increment the value.</param>
    /// <returns>A task that represents the asynchronous operation, containing the new value after incrementing.</returns>
    Task<long> IncrementAsync(string key, long value = 1);

    /// <summary>
    /// Decrements the numeric value stored at the specified key by the given amount.
    /// </summary>
    /// <param name="key">The key of the numeric value.</param>
    /// <param name="value">The amount by which to decrement the value.</param>
    /// <returns>A task that represents the asynchronous operation, containing the new value after decrementing.</returns>
    Task<long> DecrementAsync(string key, long value = 1);

    /// <summary>
    /// Retrieves a value from the cache if it exists; otherwise, obtains it via the specified acquire function, stores it in the cache, and returns it.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The key of the cached value.</param>
    /// <param name="acquire">A function that retrieves the value if it is not in the cache.</param>
    /// <param name="slidingExpiration">Optional sliding expiration timespan for the cached value.</param>
    /// <param name="absoluteExpiration">Optional absolute expiration timespan for the cached value.</param>
    /// <returns>A task that represents the asynchronous operation, containing the retrieved or newly acquired value.</returns>
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);

    /// <summary>
    /// Sets the value of a field in a hash stored at the specified key.
    /// </summary>
    /// <param name="key">The key of the hash.</param>
    /// <param name="field">The field in the hash.</param>
    /// <param name="value">The value to set for the field.</param>
    /// <returns>A task that represents the asynchronous operation, containing true if the field was set successfully; otherwise, false.</returns>
    Task<bool> HashSetAsync(string key, string field, string value);

    /// <summary>
    /// Retrieves the value of a field in a hash stored at the specified key.
    /// </summary>
    /// <param name="key">The key of the hash.</param>
    /// <param name="field">The field in the hash.</param>
    /// <returns>A task that represents the asynchronous operation, containing the value of the field if found; otherwise, null.</returns>
    Task<string?> HashGetAsync(string key, string field);

    /// <summary>
    /// Removes a field from a hash stored at the specified key.
    /// </summary>
    /// <param name="key">The key of the hash.</param>
    /// <param name="field">The field to remove from the hash.</param>
    /// <returns>A task that represents the asynchronous operation, containing true if the field was removed successfully; otherwise, false.</returns>
    Task<bool> HashRemoveAsync(string key, string field);
}