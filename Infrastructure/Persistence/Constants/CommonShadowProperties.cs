namespace CleanArchitectureTemplate.Persistence.Constants;

/// <summary>
/// Represents shadow properties that are common to all entities.
/// </summary>
internal class CommonShadowProperties
{
    public const string CreatedBy = nameof(CreatedBy);
    public const string UpdatedBy = nameof(UpdatedBy);
    public const string DeletedBy = nameof(DeletedBy);
}