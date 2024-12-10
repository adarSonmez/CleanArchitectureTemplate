namespace CommercePortal.Application.Abstractions.Storage;

/// <summary>
/// Represents the storage service interface.
/// </summary>
public interface IStorageService : IStorage
{
    /// <summary>
    /// Gets the storage name.
    /// </summary>
    string StorageName { get; }
}