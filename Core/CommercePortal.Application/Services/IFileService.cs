using Microsoft.AspNetCore.Http;

namespace CommercePortal.Application.Services;

/// <summary>
/// Represents a service for handling file operations.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Uploads a file asynchronously to the specified path.
    /// </summary>
    /// <param name="path">The path where the file will be uploaded.</param>
    /// <param name="file">The file to be uploaded.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URL or path of the uploaded file.</returns>
    Task<string> UploadFileAsync(string path, IFormFile file);

    /// <summary>
    /// Uploads multiple files asynchronously to the specified path.
    /// </summary>
    /// <param name="path">The path where the files will be uploaded.</param>
    /// <param name="files">The collection of files to be uploaded.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URLs or paths of the uploaded files.</returns>
    Task<IEnumerable<string>> UploadFilesAsync(string path, IFormFileCollection files);
}