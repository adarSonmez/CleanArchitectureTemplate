using CommercePortal.Domain.Common;

namespace CommercePortal.Domain.Constants.SmartEnums.Localizations;

/// <summary>
/// Represents a language with its ISO codes and name.
/// </summary>
public sealed class Language : Enumeration
{
    #region Properties

    public string Iso6391 { get; }
    public string Iso6392 { get; }

    #endregion Properties

    #region Constructor

    private Language(int id, string name, string iso6391, string iso6392)
        : base(id, name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(iso6391, nameof(iso6391));
        ArgumentException.ThrowIfNullOrWhiteSpace(iso6392, nameof(iso6392));

        if (iso6391.Length != 2)
        {
            throw new ArgumentException("ISO 639-1 code must be 2 characters long.", nameof(iso6391));
        }

        if (iso6392.Length != 3)
        {
            throw new ArgumentException("ISO 639-2 code must be 3 characters long.", nameof(iso6392));
        }

        Iso6391 = iso6391;
        Iso6392 = iso6392;
    }

    #endregion Constructor

    #region Predefined Languages

    #region Major Languages (101-199)

    public static readonly Language English = new(101, "English", "en", "eng");
    public static readonly Language Spanish = new(102, "Spanish", "es", "spa");
    public static readonly Language French = new(103, "French", "fr", "fra");
    public static readonly Language German = new(104, "German", "de", "deu");
    public static readonly Language Chinese = new(105, "Chinese", "zh", "zho");
    public static readonly Language Arabic = new(106, "Arabic", "ar", "ara");
    public static readonly Language Hindi = new(107, "Hindi", "hi", "hin");
    public static readonly Language Portuguese = new(108, "Portuguese", "pt", "por");
    public static readonly Language Russian = new(109, "Russian", "ru", "rus");

    #endregion Major Languages (101-199)

    #region European Languages (201-299)

    public static readonly Language Italian = new(201, "Italian", "it", "ita");
    public static readonly Language Dutch = new(202, "Dutch", "nl", "nld");
    public static readonly Language Swedish = new(203, "Swedish", "sv", "swe");
    public static readonly Language Norwegian = new(204, "Norwegian", "no", "nor");
    public static readonly Language Danish = new(205, "Danish", "da", "dan");

    #endregion European Languages (201-299)

    #region Asian Languages (301-399)

    public static readonly Language Japanese = new(301, "Japanese", "ja", "jpn");
    public static readonly Language Korean = new(302, "Korean", "ko", "kor");
    public static readonly Language Turkish = new(303, "Turkish", "tr", "tur");
    public static readonly Language Persian = new(304, "Persian", "fa", "fas");

    #endregion Asian Languages (301-399)

    #region African Languages (401-499)

    public static readonly Language Swahili = new(401, "Swahili", "sw", "swa");
    public static readonly Language Afrikaans = new(402, "Afrikaans", "af", "afr");
    public static readonly Language Hausa = new(403, "Hausa", "ha", "hau");

    #endregion African Languages (401-499)

    #endregion Predefined Languages
}