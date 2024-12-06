using CommercePortal.Application.Services;
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

    /// <inheritdoc/>
    public async Task<string> UploadFileAsync(string path, IFormFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return Path.Combine(path, uniqueFileName).Replace("\\", "/");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> UploadFilesAsync(string path, IFormFileCollection files)
    {
        ArgumentNullException.ThrowIfNull(files);

        var filePaths = new List<string>();
        foreach (var file in files)
        {
            filePaths.Add(await UploadFileAsync(path, file));
        }
        return filePaths;
    }
}