using MediatR;

namespace CleanArchitectureTemplate.Application.Utilities;

/// <summary>
/// Utility class for entity-related operations.
/// /// </summary>
public static class EntityHelper
{
    /// <summary>
    /// Updates the properties of the target entity with the values from the source entity, ignoring null values.
    /// </summary>
    /// typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="sourceRequest">The source request containing the values to be mapped.</param>
    /// <param name="targetEntity">The target entity to be updated.</param>
    public static void MapNonNullProperties<TEntity>(IBaseRequest sourceRequest, TEntity targetEntity)
    {
        var properties = typeof(TEntity).GetProperties();
        foreach (var property in properties)
        {
            var sourceValue = sourceRequest.GetType().GetProperty(property.Name)?.GetValue(sourceRequest);
            if (sourceValue != null)
            {
                try
                {
                    property.SetValue(targetEntity, sourceValue);
                }
                catch
                {
                    // ignored (will be handled by the caller)
                }
            }
        }
    }
}