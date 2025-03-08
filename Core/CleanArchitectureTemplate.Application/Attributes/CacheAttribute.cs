namespace CleanArchitectureTemplate.Application.Attributes;

/// <summary>
/// Cache attribute to be used on methods that should be cached.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class CacheAttribute : Attribute
{
    public TimeSpan? SlidingExpiration { get; }
    public TimeSpan? AbsoluteExpiration { get; }

    public CacheAttribute(int slidingExpirationSeconds)
    {
        SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSeconds);
    }

    public CacheAttribute(int slidingExpirationSeconds, int absoluteExpirationSeconds)
    {
        SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSeconds);
        AbsoluteExpiration = TimeSpan.FromSeconds(absoluteExpirationSeconds);
    }
}