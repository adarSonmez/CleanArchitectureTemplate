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
}