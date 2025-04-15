namespace CleanArchitectureTemplate.Application.Constants.StringConstants;

/// <summary>
/// Convenience class providing compile-time names for standard exchange types.
/// </summary>
/// <remarks>
/// Use the static members of this class as values for the
/// "exchangeType" arguments for IChannel methods such as
/// ExchangeDeclare. The broker may be extended with additional
/// exchange types that do not appear in this class.
/// </remarks>
public static class ExchangeTypes
{
    /// <summary>
    /// Exchange type used for AMQP direct exchanges.
    /// </summary>
    public const string Direct = "direct";

    /// <summary>
    /// Exchange type used for AMQP fanout exchanges.
    /// </summary>
    public const string Fanout = "fanout";

    /// <summary>
    /// Exchange type used for AMQP headers exchanges.
    /// </summary>
    public const string Headers = "headers";

    /// <summary>
    /// Exchange type used for AMQP topic exchanges.
    /// </summary>
    public const string Topic = "topic";
}