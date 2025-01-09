using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Application.Abstractions.Services.Storage;

/// <summary>
/// Represents the storage interface for all storage types. (local, cloud, etc.)
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Deletes a file asynchronously from the specified path.
    /// </summary>
    /// <param name="path">Te path or container where the file will be deleted.</param>
    /// <param name="fileName">The name of the file to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteFileAsync(string path, string fileName);

    /// <summary>
    /// Specifies whether the file exists asynchronously in the specified path.
    /// </summary>
    /// <param name="path">The path or container where the file will be checked.</param>
    /// <param name="fileName">The name of the file to be checked.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a flag indicating whether the file exists.</returns>
    Task<bool> HasFileAsync(string path, string fileName);

    /// <summary>
    /// Gets the file asynchronously from the specified path.
    /// </summary>
    /// <param name="path">The path or container where the file will be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file content.</returns>
    Task<IEnumerable<string>> GetFilesAsync(string path);

    /// <summary>
    /// Uploads a file asynchronously to the specified path.
    /// </summary>
    /// <param name="path">The path or container where the file will be uploaded.</param>
    /// <param name="file">The file to be uploaded.</param>
    /// <param name="useGuid">A flag indicating whether to use a GUID in the file name.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the folder and name of the uploaded file.</returns>
    Task<(string Folder, string Name, long Size)> UploadFileAsync(string path, IFormFile file, bool useGuid = true);

    /// <summary>
    /// Uploads multiple files asynchronously to the specified path.
    /// </summary>
    /// <param name="path">The path or container where the files will be uploaded.</param>
    /// <param name="files">The collection of files to be uploaded.</param>
    /// <param name="useGuid">A flag indicating whether to use a GUID in the file name.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the folder name, file name, and size of the uploaded files.</returns>
    Task<IEnumerable<(string Folder, string Name, long Size)>> UploadFilesAsync(string path, IFormFileCollection files, bool useGuid = true);
}