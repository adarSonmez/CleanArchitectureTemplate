namespace CleanArchitectureTemplate.Domain.Constants.Enums;

/// <summary>
/// Represents the type of storage.
/// </summary>
public enum StorageType
{
    LocalStorage,
    AzureStorage,
    S3Storage,
    GoogleCloudStorage,
    DropboxStorage,
    OneDriveStorage
}