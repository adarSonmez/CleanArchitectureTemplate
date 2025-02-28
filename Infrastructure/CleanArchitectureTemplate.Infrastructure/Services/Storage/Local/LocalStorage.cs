using CleanArchitectureTemplate.Application.Abstractions.Services.Storage.Local;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Infrastructure.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Infrastructure.Services.Storage.Local;

public class LocalStorage : Storage, ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #region ISorage Implementation

    /// <inheritdoc/>
    public async Task<(string Folder, string Name, long Size)> UploadFileAsync(string path, IFormFile file, bool useGuid = true)
    {
        if (file == null)
            throw new ValidationFailedException("File is required.");

        var uploadsFolder = GetUploadFolderPath(path);
        FileHelper.EnsureDirectoryExists(uploadsFolder);

        var fileName = await GenerateUniqueFileName(uploadsFolder, file, HasFileAsync, useGuid);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return (Folder: path, Name: fileName, Size: file.Length);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<(string Folder, string Name, long Size)>> UploadFilesAsync(string path, IFormFileCollection files, bool useGuid = true)
    {
        if (files == null || files.Count == 0)
            throw new ValidationFailedException("Files are required.");

        var filePaths = new List<(string Path, string Name, long Size)>();
        foreach (var file in files)
        {
            var uploadedPath = await UploadFileAsync(path, file, useGuid);
            filePaths.Add(uploadedPath);
        }

        return filePaths;
    }

    /// <inheritdoc/>
    public async Task DeleteFileAsync(string path, string fileName)
    {
        await Task.Run(() =>
        {
            var filePath = Path.Combine(path, fileName);
            FileHelper.DeleteFileIfExists(filePath);
        });
    }

    /// <inheritdoc/>
    public async Task<bool> HasFileAsync(string path, string fileName)
    {
        var filePath = Path.Combine(path, fileName);
        return await Task.Run(() => File.Exists(filePath));
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetFilesAsync(string path) => await Task.Run(() => Directory.GetFiles(path).AsEnumerable());

    #endregion ISorage Implementation

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