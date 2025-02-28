using CleanArchitectureTemplate.Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Infrastructure.Services.Storage;

/// <summary>
/// Provides base functionality for storage services.
/// </summary>
public abstract class Storage
{
    /// <summary>
    /// Delegate check if a file exists at the specified path.
    /// </summary>
    /// <param name="path">The path to the directory.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the file exists.</returns>
    public delegate Task<bool> HasFile(string path, string fileName);

    /// <summary>
    /// Generates a unique file path by appending a counter if a file with the same name already exists.
    /// </summary>
    /// <param name="directory">The target directory.</param>
    /// <param name="file">The file being saved.</param>
    /// <param name="useGuid">Whether to append a GUID to the file name.</param>
    /// <returns>A unique file path for the file.</returns>
    public async Task<string> GenerateUniqueFileName(string directory, IFormFile file, HasFile hasFile, bool useGuid)
    {
        var fileName = StringNormalizer.NormalizeFileName(file.FileName);

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var fileExtension = Path.GetExtension(fileName);

        if (useGuid)
        {
            return $"{fileNameWithoutExtension}_{Guid.NewGuid()}{fileExtension}";
        }

        if (!await hasFile(directory, fileName))
        {
            return fileName;
        }

        var counter = 1;

        while (await hasFile(directory, fileName))
        {
            fileName = StringNormalizer.NormalizeFileName($"{fileNameWithoutExtension} ({counter}){fileExtension}");
            counter++;
        }

        return fileName;
    }
}