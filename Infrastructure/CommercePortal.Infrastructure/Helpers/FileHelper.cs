using Microsoft.AspNetCore.Http;

namespace CommercePortal.Infrastructure.Helpers;

/// <summary>
/// Utility class for file-related operations.
/// </summary>
public static class FileHelper
{
    /// <summary>
    /// Ensures the specified directory exists, creating it if necessary.
    /// </summary>
    /// <param name="directory">The directory path.</param>
    public static void EnsureDirectoryExists(string directory)
    {
        if (!Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to create directory: {directory}", ex);
            }
        }
    }

    /// <summary>
    /// Deletes a file safely if it exists.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    public static void DeleteFileIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to delete file: {filePath}", ex);
            }
        }
    }

    /// <summary>
    /// Checks if a file is locked (in use by another process).
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>True if the file is locked, false otherwise.</returns>
    public static bool IsFileLocked(string filePath)
    {
        try
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            return false;
        }
        catch (IOException)
        {
            return true;
        }
    }

    /// <summary>
    /// Generates a unique file path by appending a counter if a file with the same name already exists.
    /// </summary>
    /// <param name="directory">The target directory.</param>
    /// <param name="file">The file being saved.</param>
    /// <param name="useGuid">Whether to append a GUID to the file name.</param>
    /// <returns>A unique file path for the file.</returns>
    public static string GenerateLocalUniqueFilePath(string directory, IFormFile file, bool useGuid)
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
}