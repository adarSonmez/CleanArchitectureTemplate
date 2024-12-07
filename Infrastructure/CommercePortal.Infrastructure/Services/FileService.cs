using CommercePortal.Application.Services;
using CommercePortal.Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CommercePortal.Infrastructure.Services;

/// <summary>
/// Implementation of <see cref="IFileService"/> that handles file operations.
/// </summary>
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
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

        var filePath = GenerateUniqueFilePath(uploadsFolder, file, useGuid);
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

    /// <summary>
    /// Generates a unique file path by appending a counter if a file with the same name already exists.
    /// </summary>
    /// <param name="directory">The target directory.</param>
    /// <param name="file">The file being saved.</param>
    /// <param name="useGuid">Whether to append a GUID to the file name.</param>
    /// <returns>A unique file path for the file.</returns>
    private string GenerateUniqueFilePath(string directory, IFormFile file, bool useGuid)
    {
        var fileName = StringNormalizer.NormalizeFileName(file.FileName);
        var filePath = Path.Combine(directory, fileName);

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var fileExtension = Path.GetExtension(fileName);

        if (useGuid)
        {
            return Path.Combine(directory, $"{fileNameWithoutExtension}_{Guid.NewGuid()}{fileExtension}");
        }

        if (!File.Exists(filePath))
        {
            return filePath;
        }

        var counter = 1;

        while (File.Exists(filePath))
        {
            fileName = $"{fileNameWithoutExtension} ({counter}){fileExtension}";
            filePath = Path.Combine(directory, StringNormalizer.NormalizeFileName(fileName));
            counter++;
        }

        return filePath;
    }

    #endregion Private Methods
}