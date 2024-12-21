using CommercePortal.Domain.Common;

namespace CommercePortal.Domain.Constants.SmartEnums.Localization;

/// <summary>
/// Represents a country with its ISO codes and name.
/// </summary>
public sealed class Country : Enumeration
{
    #region Properties

    public string IsoAlpha2 { get; }
    public string IsoAlpha3 { get; }
    public int NumericCode { get; }

    #endregion Properties

    #region Constructor

    private Country(int id, string name, string isoAlpha2, string isoAlpha3, int numericCode)
        : base(id, name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(isoAlpha2, nameof(isoAlpha2));
        ArgumentException.ThrowIfNullOrWhiteSpace(isoAlpha3, nameof(isoAlpha3));

        if (isoAlpha2.Length != 2)
        {
            throw new ArgumentException("ISO Alpha-2 code must be 2 characters long.", nameof(isoAlpha2));
        }

        if (isoAlpha3.Length != 3)
        {
            throw new ArgumentException("ISO Alpha-3 code must be 3 characters long.", nameof(isoAlpha3));
        }

        IsoAlpha2 = isoAlpha2;
        IsoAlpha3 = isoAlpha3;
        NumericCode = numericCode;
    }

    #endregion Constructor

    #region Predefined Countries

    #region North America (101-199)

    public static readonly Country Usa = new(101, "United States of America", "US", "USA", 840);
    public static readonly Country Can = new(102, "Canada", "CA", "CAN", 124);
    public static readonly Country Mex = new(103, "Mexico", "MX", "MEX", 484);

    #endregion North America (101-199)

    #region Europe (201-299)

    public static readonly Country Germany = new(201, "Germany", "DE", "DEU", 276);
    public static readonly Country France = new(202, "France", "FR", "FRA", 250);
    public static readonly Country Uk = new(203, "United Kingdom", "GB", "GBR", 826);
    public static readonly Country Italy = new(204, "Italy", "IT", "ITA", 380);
    public static readonly Country Spain = new(205, "Spain", "ES", "ESP", 724);

    #endregion Europe (201-299)

    #region Asia (301-399)

    public static readonly Country China = new(301, "China", "CN", "CHN", 156);
    public static readonly Country Japan = new(302, "Japan", "JP", "JPN", 392);
    public static readonly Country India = new(303, "India", "IN", "IND", 356);
    public static readonly Country SouthKorea = new(304, "South Korea", "KR", "KOR", 410);
    public static readonly Country Turkey = new(305, "Türkiye", "TR", "TUR", 792);

    #endregion Asia (301-399)

    #region South America (401-499)

    public static readonly Country Brazil = new(401, "Brazil", "BR", "BRA", 76);
    public static readonly Country Argentina = new(402, "Argentina", "AR", "ARG", 32);
    public static readonly Country Colombia = new(403, "Colombia", "CO", "COL", 170);

    #endregion South America (401-499)

    #region Africa (501-599)

    public static readonly Country SouthAfrica = new(501, "South Africa", "ZA", "ZAF", 710);
    public static readonly Country Egypt = new(502, "Egypt", "EG", "EGY", 818);
    public static readonly Country Nigeria = new(503, "Nigeria", "NG", "NGA", 566);

    #endregion Africa (501-599)

    #region Oceania (601-699)

    public static readonly Country Australia = new(601, "Australia", "AU", "AUS", 36);
    public static readonly Country NewZealand = new(602, "New Zealand", "NZ", "NZL", 554);
    public static readonly Country Fiji = new(603, "Fiji", "FJ", "FJI", 242);

    #endregion Oceania (601-699)

    #endregion Predefined Countries
}