using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CommercePortal.Application.Abstractions.Services.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CommercePortal.Infrastructure.Services.Storage.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    #region Private Fields

    private readonly BlobServiceClient _blobServiceClient;

    #endregion Private Fields

    #region Constructors

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
    }

    #endregion Constructors

    #region IStorage Implementation

    /// <inheritdoc/>
    public async Task DeleteFileAsync(string path, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(path);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetFilesAsync(string path)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(path);
        var blobs = containerClient.GetBlobs();
        return await Task.FromResult(blobs.Select(blob => blob.Name));
    }

    /// <inheritdoc/>
    public async Task<bool> HasFileAsync(string path, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(path);
        var blobClient = containerClient.GetBlobClient(fileName);
        return await blobClient.ExistsAsync();
    }

    /// <inheritdoc/>
    public async Task<(string Folder, string Name, long Size)> UploadFileAsync(string path, IFormFile file, bool useGuid = true)
    {
        ArgumentNullException.ThrowIfNull(file);

        var containerClient = _blobServiceClient.GetBlobContainerClient(path);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var fileName = await GenerateUniqueFileName(path, file, HasFileAsync, useGuid);
        var blobClient = containerClient.GetBlobClient(fileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        return (Folder: path, Name: fileName, Size: file.Length);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<(string Folder, string Name, long Size)>> UploadFilesAsync(string path, IFormFileCollection files, bool useGuid = true)
    {
        ArgumentNullException.ThrowIfNull(files);

        var filePaths = new List<(string Path, string Name, long Size)>();
        foreach (var file in files)
        {
            var uploadedPath = await UploadFileAsync(path, file, useGuid);
            filePaths.Add(uploadedPath);
        }

        return filePaths;
    }

    #endregion IStorage Implementation
}