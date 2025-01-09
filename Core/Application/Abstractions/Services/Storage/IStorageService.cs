using CleanArchitectureTemplate.Domain.Constants.Enums;

namespace CleanArchitectureTemplate.Application.Abstractions.Services.Storage;

/// <summary>
/// Represents the storage service interface.
/// </summary>
public interface IStorageService : IStorage
{
    /// <summary>
    /// Gets the storage name.
    /// </summary>
    string StorageName { get; }

    /// <summary>
    /// Gets the storage type.
    /// </summary>
    StorageType StorageType => Enum.Parse<StorageType>(StorageName);
}