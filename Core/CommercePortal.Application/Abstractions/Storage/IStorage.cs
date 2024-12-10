using Microsoft.AspNetCore.Http;

namespace CommercePortal.Application.Abstractions.Storage;

/// <summary>
/// Represents the storage interface for all storage types. (local, cloud, etc.)
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Deletes a file asynchronously from the specified path.
    /// </summary>
    /// <param name="path">Te path or container where the file will be deleted.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteFileAsync(string path);

    /// <summary>
    /// Specifies whether the file exists asynchronously in the specified path.
    /// </summary>
    /// <param name="path">The path or container where the file will be checked.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a flag indicating whether the file exists.</returns>
    Task<bool> HasFileAsync(string path);

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
    /// <returns>A task that represents the asynchronous operation. The task result contains the URL or path of the uploaded file.</returns>
    Task<string> UploadFileAsync(string path, IFormFile file, bool useGuid = true);

    /// <summary>
    /// Uploads multiple files asynchronously to the specified path.
    /// </summary>
    /// <param name="path">The path or container where the files will be uploaded.</param>
    /// <param name="files">The collection of files to be uploaded.</param>
    /// <param name="useGuid">A flag indicating whether to use a GUID in the file name.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URLs or paths of the uploaded files.</returns>
    Task<IEnumerable<string>> UploadFilesAsync(string path, IFormFileCollection files, bool useGuid = true);
}