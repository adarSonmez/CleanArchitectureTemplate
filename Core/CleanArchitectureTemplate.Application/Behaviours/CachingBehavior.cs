using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Attributes;
using MediatR;
using System.Reflection;

namespace CleanArchitectureTemplate.Application.Behaviours;

/// <summary>
/// Defines a caching behavior for MediatR requests.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICacheService _cacheService;

    public CachingBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Check if the CacheAttribute is applied
        var cacheAttribute = request.GetType().GetCustomAttribute<CacheAttribute>();
        if (cacheAttribute == null)
        {
            // No caching; proceed to the next behavior in the pipeline
            return await next();
        }

        // Generate a cache key based on request type and contents
        var cacheKey = GenerateCacheKey(request);

        // Attempt to get the cached response
        var cachedResponse = await _cacheService.GetAsync<TResponse>(cacheKey);
        if (cachedResponse != null)
        {
            return cachedResponse;
        }

        // Proceed with the handler
        var response = await next();

        // Cache the response
        await _cacheService.SetAsync(cacheKey, response, cacheAttribute.SlidingExpiration, cacheAttribute.AbsoluteExpiration);

        return response;
    }

    private string GenerateCacheKey(TRequest request)
    {
        var requestType = request.GetType();
        var properties = requestType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var keyBuilder = new System.Text.StringBuilder();
        keyBuilder.Append(requestType.Name);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(request);
            keyBuilder.Append($"|{prop.Name}:{value}");
        }

        return keyBuilder.ToString();
    }
}