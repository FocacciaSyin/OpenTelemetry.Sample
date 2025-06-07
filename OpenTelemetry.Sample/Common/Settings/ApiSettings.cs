namespace Common.Settings;

public class ApiSettings
{
    /// <summary>
    /// WebServiceURL
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Opentelemetry Endpoint URL
    /// </summary>
    public string OTLP_ENDPOINT_URL { get; set; }

    /// <summary>
    /// Tracing Service Name
    /// </summary>
    public string OTLP_ServiceName { get; set; }

    /// <summary>
    /// Tracing Service Version
    /// </summary>
    public string OTLP_Version { get; set; }
}
