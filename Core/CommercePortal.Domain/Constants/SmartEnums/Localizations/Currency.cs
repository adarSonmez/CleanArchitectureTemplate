using CommercePortal.Domain.Common;

namespace CommercePortal.Domain.Constants.SmartEnums.Localizations;

/// <summary>
/// Represents a currency with its ISO code, name, and symbol.
/// </summary>
public sealed class Currency : Enumeration
{
    #region Properties

    public string IsoCode { get; }
    public string Symbol { get; }

    #endregion Properties

    #region Constructor

    private Currency(int id, string name, string isoCode, string symbol)
        : base(id, name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(isoCode, nameof(isoCode));
        ArgumentException.ThrowIfNullOrWhiteSpace(symbol, nameof(symbol));

        if (isoCode.Length != 3)
        {
            throw new ArgumentException("ISO code must be 3 characters long.", nameof(isoCode));
        }

        IsoCode = isoCode;
        Symbol = symbol;
    }

    #endregion Constructor

    #region Predefined Currencies

    #region Major Currencies (101-199)

    public static readonly Currency Usd = new(101, "United States Dollar", "USD", "$");
    public static readonly Currency Eur = new(102, "Euro", "EUR", "€");
    public static readonly Currency Gbp = new(103, "British Pound Sterling", "GBP", "£");
    public static readonly Currency Jpy = new(104, "Japanese Yen", "JPY", "¥");
    public static readonly Currency Try = new(105, "Turkish Lira", "TRY", "₺");
    public static readonly Currency Cny = new(106, "Chinese Yuan", "CNY", "¥");
    public static readonly Currency Aud = new(107, "Australian Dollar", "AUD", "A$");
    public static readonly Currency Cad = new(108, "Canadian Dollar", "CAD", "C$");
    public static readonly Currency Chf = new(109, "Swiss Franc", "CHF", "CHF");

    #endregion Major Currencies (101-199)

    #region Emerging Market Currencies (201-299)

    public static readonly Currency Rub = new(201, "Russian Ruble", "RUB", "₽");
    public static readonly Currency Inr = new(202, "Indian Rupee", "INR", "₹");
    public static readonly Currency Zar = new(203, "South African Rand", "ZAR", "R");
    public static readonly Currency Brl = new(204, "Brazilian Real", "BRL", "R$");
    public static readonly Currency Mxn = new(205, "Mexican Peso", "MXN", "$");

    #endregion Emerging Market Currencies (201-299)

    #region Cryptocurrencies (301-399)

    public static readonly Currency Btc = new(301, "Bitcoin", "BTC", "₿");
    public static readonly Currency Eth = new(302, "Ethereum", "ETH", "Ξ");
    public static readonly Currency Usdt = new(303, "Tether", "USDT", "₮");
    public static readonly Currency Bnb = new(304, "Binance Coin", "BNB", "₿");
    public static readonly Currency Ada = new(305, "Cardano", "ADA", "₳");
    public static readonly Currency Add = new(306, "Avalanche", "ADD", "⨠");
    public static readonly Currency Sol = new(307, "Solana", "SOL", "S◎");
    public static readonly Currency Dot = new(308, "Polkadot", "DOT", "◆");
    public static readonly Currency Doge = new(309, "Dogecoin", "DOGE", "Ð");
    public static readonly Currency Ltc = new(310, "Litecoin", "LTC", "Ł");

    #endregion Cryptocurrencies (301-399)

    #region Other Currencies (401-499)

    public static readonly Currency Sgd = new(401, "Singapore Dollar", "SGD", "S$");
    public static readonly Currency Hkd = new(402, "Hong Kong Dollar", "HKD", "HK$");
    public static readonly Currency Krw = new(403, "South Korean Won", "KRW", "₩");
    public static readonly Currency Nok = new(404, "Norwegian Krone", "NOK", "kr");
    public static readonly Currency Sek = new(405, "Swedish Krona", "SEK", "kr");

    #endregion Other Currencies (401-499)

    #endregion Predefined Currencies

    #region Static Methods

    /// <summary>
    /// Gets the currency instance with the specified ISO code.
    /// </summary>
    public static Currency FromIsoCode(string isoCode)
    {
        var matchingItem = GetAll<Currency>().FirstOrDefault(e => string.Equals(e.IsoCode, isoCode, StringComparison.OrdinalIgnoreCase));

        return matchingItem ?? throw new ArgumentException($"No currency with ISO code {isoCode} found.", nameof(isoCode));
    }

    #endregion Static Methods
}