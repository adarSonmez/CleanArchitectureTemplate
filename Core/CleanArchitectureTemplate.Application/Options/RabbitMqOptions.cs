namespace CleanArchitectureTemplate.Application.Options;

/// <summary>
/// Options used to configure RabbitMQ connection and messaging behavior.
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// The RabbitMQ host name.
    /// </summary>
    public string HostName { get; set; } = "localhost";

    /// <summary>
    /// The port number for RabbitMQ
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// The username for RabbitMQ authentication.
    /// </summary>
    public string UserName { get; set; } = "guest";

    /// <summary>
    /// The password for RabbitMQ authentication.
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// The virtual host to use for RabbitMQ connections.
    /// </summary>
    public string VirtualHost { get; set; } = "/";
}