using CommercePortal.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace CommercePortal.Infrastructure.Services.Storage;

/// <summary>
/// Represents the storage service which provides methods to interact with the injected storage implementation.
/// </summary>
public class StorageService : IStorageService
{
    #region Private Fields

    private readonly IStorage _storage;

    #endregion Private Fields

    #region Constructors

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    #endregion Constructors

    #region IStorageService Implementation

    /// <inheritdoc />
    public string StorageName => _storage.GetType().Name;

    /// <inheritdoc />
    public async Task DeleteFileAsync(string path, string fileName) => await _storage.DeleteFileAsync(path, fileName);

    /// <inheritdoc />
    public async Task<IEnumerable<string>> GetFilesAsync(string path) => await _storage.GetFilesAsync(path);

    /// <inheritdoc />
    public async Task<bool> HasFileAsync(string path, string fileName) => await _storage.HasFileAsync(path, fileName);

    /// <inheritdoc />
    public async Task<string> UploadFileAsync(string path, IFormFile file, bool useGuid = true) => await _storage.UploadFileAsync(path, file, useGuid);

    /// <inheritdoc />
    public async Task<IEnumerable<string>> UploadFilesAsync(string path, IFormFileCollection files, bool useGuid = true) => await _storage.UploadFilesAsync(path, files, useGuid);

    #endregion IStorageService Implementation
}