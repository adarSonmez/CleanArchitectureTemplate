using CommercePortal.Application.Abstractions.Storage.Local;
using CommercePortal.Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CommercePortal.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #region Public Methods

    /// <inheritdoc/>
    public async Task<string> UploadFileAsync(string path, IFormFile file, bool useGuid = true)
    {
        ArgumentNullException.ThrowIfNull(file);

        var uploadsFolder = GetUploadFolderPath(path);
        FileHelper.EnsureDirectoryExists(uploadsFolder);

        var filePath = FileHelper.GenerateLocalUniqueFilePath(uploadsFolder, file, useGuid);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return filePath;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> UploadFilesAsync(string path, IFormFileCollection files, bool useGuid = true)
    {
        ArgumentNullException.ThrowIfNull(files);

        var filePaths = new List<string>();
        foreach (var file in files)
        {
            var uploadedPath = await UploadFileAsync(path, file, useGuid);
            filePaths.Add(uploadedPath);
        }

        return filePaths;
    }

    /// <inheritdoc/>
    public async Task DeleteFileAsync(string path) => await Task.Run(() => FileHelper.DeleteFileIfExists(path));

    /// <inheritdoc/>
    public async Task<bool> HasFileAsync(string path) => await Task.Run(() => File.Exists(path));

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetFilesAsync(string path) => await Task.Run(() => Directory.GetFiles(path).AsEnumerable());

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Resolves the full upload folder path.
    /// </summary>
    /// <param name="path">Relative path where the file should be uploaded.</param>
    /// <returns>The full folder path.</returns>
    private string GetUploadFolderPath(string path)
    {
        return Path.Combine(_webHostEnvironment.WebRootPath, path);
    }

    #endregion Private Methods
}