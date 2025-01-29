using CleanArchitectureTemplate.Domain.Common;

namespace CleanArchitectureTemplate.Domain.Constants.SmartEnums.Files;

/// <summary>
/// Represents a file extension.
/// </summary>
public sealed class FileExtension : Enumeration
{
    #region Properties

    /// <summary>
    /// Gets the file extension.
    /// </summary>
    public string Extension { get; }

    #endregion Properties

    #region Constructor

    private FileExtension(int id, string name, string extension) : base(id, name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(extension, nameof(extension));

        Extension = extension;
    }

    #endregion Constructor

    #region Static Converters

    /// <summary>
    /// Gets the enumeration instance with the specified extension.
    /// </summary>
    /// <param name="extension">The extension to search for.</param>"
    /// <returns>The matching enumeration instance.</returns>
    public static FileExtension FromExtension(string extension)
    {
        extension = extension.ToLowerInvariant();
        var matchingItem = GetAll<FileExtension>().FirstOrDefault(e => string.Equals(e.Extension, extension, StringComparison.OrdinalIgnoreCase));

        return matchingItem ?? throw new ArgumentException($"No {nameof(FileExtension)} with Extension {extension} found.");
    }

    #endregion Static Converters

    #region Predefined Extensions

    #region Images

    public static readonly FileExtension Jpg = new(101, "JPEG Image", ".jpg");
    public static readonly FileExtension Png = new(102, "PNG Image", ".png");
    public static readonly FileExtension Gif = new(103, "GIF Image", ".gif");
    public static readonly FileExtension Bmp = new(104, "Bitmap Image", ".bmp");
    public static readonly FileExtension Svg = new(105, "Scalable Vector Graphics", ".svg");

    #endregion Images

    #region Documents

    public static readonly FileExtension Pdf = new(201, "PDF Document", ".pdf");
    public static readonly FileExtension Doc = new(202, "Microsoft Word Document", ".doc");
    public static readonly FileExtension Docx = new(203, "Microsoft Word Open XML Document", ".docx");
    public static readonly FileExtension Rtf = new(204, "Rich Text Format", ".rtf");
    public static readonly FileExtension Txt = new(205, "Text Document", ".txt");

    #endregion Documents

    #region Spreadsheets

    public static readonly FileExtension Csv = new(301, "Comma-Separated Values", ".csv");
    public static readonly FileExtension Xls = new(302, "Microsoft Excel Spreadsheet", ".xls");
    public static readonly FileExtension Xlsx = new(303, "Microsoft Excel Open XML Spreadsheet", ".xlsx");
    public static readonly FileExtension Ods = new(304, "OpenDocument Spreadsheet", ".ods");

    #endregion Spreadsheets

    #region Presentations

    public static readonly FileExtension Ppt = new(401, "Microsoft PowerPoint Presentation", ".ppt");
    public static readonly FileExtension Pptx = new(402, "Microsoft PowerPoint Open XML Presentation", ".pptx");
    public static readonly FileExtension Key = new(403, "Apple Keynote Presentation", ".key");
    public static readonly FileExtension Odp = new(404, "OpenDocument Presentation", ".odp");

    #endregion Presentations

    #region Archives

    public static readonly FileExtension Zip = new(501, "ZIP Archive", ".zip");
    public static readonly FileExtension Rar = new(502, "RAR Archive", ".rar");
    public static readonly FileExtension Tar = new(503, "Tape Archive", ".tar");
    public static readonly FileExtension Gz = new(504, "Gzip Compressed Archive", ".gz");
    public static readonly FileExtension Gzf = new(505, "Gzip Compressed Archive", ".gzf");

    #endregion Archives

    #region Audio

    public static readonly FileExtension Mp3 = new(601, "MP3 Audio", ".mp3");
    public static readonly FileExtension Wav = new(602, "Waveform Audio", ".wav");
    public static readonly FileExtension Flac = new(603, "Free Lossless Audio Codec", ".flac");
    public static readonly FileExtension Ogg = new(604, "OGG Audio", ".ogg");
    public static readonly FileExtension Aac = new(605, "Advanced Audio Coding", ".aac");

    #endregion Audio

    #region Video

    public static readonly FileExtension Mp4 = new(701, "MPEG-4 Video", ".mp4");
    public static readonly FileExtension Mkv = new(702, "Matroska Video", ".mkv");
    public static readonly FileExtension Avi = new(703, "Audio Video Interleave", ".avi");
    public static readonly FileExtension Mov = new(704, "Apple QuickTime Movie", ".mov");
    public static readonly FileExtension Wmv = new(705, "Windows Media Video", ".wmv");

    #endregion Video

    #region Code

    public static readonly FileExtension Cs = new(801, "C# Source Code", ".cs");
    public static readonly FileExtension Js = new(802, "JavaScript Source Code", ".js");
    public static readonly FileExtension Html = new(803, "HyperText Markup Language", ".html");
    public static readonly FileExtension Css = new(804, "Cascading Style Sheets", ".css");
    public static readonly FileExtension Json = new(805, "JavaScript Object Notation", ".json");
    public static readonly FileExtension Xml = new(806, "Extensible Markup Language", ".xml");
    public static readonly FileExtension Yaml = new(807, "YAML Ain't Markup Language", ".yaml");
    public static readonly FileExtension Xaml = new(808, "Extensible Application Markup Language", ".xaml");
    public static readonly FileExtension Sql = new(809, "Structured Query Language", ".sql");
    public static readonly FileExtension Php = new(810, "PHP Source Code", ".php");

    #endregion Code

    #region System Files

    public static readonly FileExtension Exe = new(901, "Executable File", ".exe");
    public static readonly FileExtension Dll = new(902, "Dynamic Link Library", ".dll");
    public static readonly FileExtension Sys = new(903, "System File", ".sys");
    public static readonly FileExtension Ini = new(904, "Initialization File", ".ini");
    public static readonly FileExtension Bin = new(905, "Binary File", ".bin");
    public static readonly FileExtension Dat = new(906, "Data File", ".dat");
    public static readonly FileExtension Db = new(907, "Database File", ".db");
    public static readonly FileExtension Dbf = new(908, "Database File", ".dbf");
    public static readonly FileExtension Mdb = new(909, "Microsoft Access Database", ".mdb");
    public static readonly FileExtension Accdb = new(910, "Microsoft Access Database", ".accdb");

    #endregion System Files

    #region Miscellaneous

    public static readonly FileExtension Iso = new(1001, "ISO Image", ".iso");
    public static readonly FileExtension Dmg = new(1002, "Apple Disk Image", ".dmg");
    public static readonly FileExtension Log = new(1003, "Log File", ".log");

    #endregion Miscellaneous

    #endregion Predefined Extensions
}