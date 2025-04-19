using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Messaging.Declaration;

/// <summary>
/// Represents the declaration options for an exchange.
/// </summary>
/// <param name="ExchangeName">The name of the exchange.</param>
/// <param name="ExchangeType">The type of the exchange (direct, fanout, topic, headers).</param>
/// <param name="Durable">Specifies whether the exchange should survive a broker restart.</param>
/// <param name="AutoDelete">Indicates whether the exchange should be automatically deleted when not in use.</param>
/// <param name="Arguments">Additional arguments for advanced exchange configuration.</param>
public record ExchangeDeclarationDto(
    string ExchangeName,
    string ExchangeType,
    bool Durable = true,
    bool AutoDelete = false,
    IDictionary<string, object?>? Arguments = null
) : IDto;