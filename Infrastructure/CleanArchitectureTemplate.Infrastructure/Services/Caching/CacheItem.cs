namespace CleanArchitectureTemplate.Infrastructure.Services.Caching;

/// <summary>
/// Represents a cache item.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class CacheItem<T>
{
    public T? Value { get; set; }
    public DateTime? AbsoluteExpiration { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
}