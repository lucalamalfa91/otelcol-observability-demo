using System.ComponentModel.DataAnnotations;

namespace weather_forecast;

public class OpenTelemetryServiceConfig
{
    public const string Section = "OpenTelemetryServiceConfig";
    
    [Required] public required string ServiceName { get; set; }
    [Required] public required string Endpoint { get; set; }
    [Required] public required string ExporterType { get; set; }
}