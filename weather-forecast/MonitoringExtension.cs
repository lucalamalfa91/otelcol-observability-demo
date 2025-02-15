using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace weather_forecast
{
    public static class MonitoringExtensions
    {
        public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder, IConfiguration config)
        {
            var openTelemetryServiceConfig = config.Get<OpenTelemetryServiceConfig>() ??
                                             throw new ArgumentException("open telemetry parameters are not defined.");
            
            //change openTelemetryServiceConfig.ExporterType = otlp in appsettings to use the OTLP configuration exporter 
            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource
                    .AddService(
                        serviceName: openTelemetryServiceConfig.ServiceName,
                        serviceVersion: System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3) ??
                                        "unknown", // SemVer
                        serviceInstanceId: Environment.MachineName))
                .WithLogging(logBuilder =>
                    logBuilder.AddOtlpExporter(otlpOptions =>
                        otlpOptions.Endpoint = new Uri(openTelemetryServiceConfig.Endpoint)),
                        options =>
                        {
                            options.IncludeScopes = true;
                            options.IncludeFormattedMessage = true;
                            options.ParseStateValues = true;
                        }
                )
                .WithTracing(traceBuilder =>
                {
                    traceBuilder
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(otlpOptions =>
                        {
                            otlpOptions.Endpoint = new Uri(openTelemetryServiceConfig.Endpoint);
                        });
                })
                .WithMetrics(metricBuilder =>
                {
                    metricBuilder
                        .AddRuntimeInstrumentation()
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(otlpOptions =>
                        {
                            otlpOptions.Endpoint = new Uri(openTelemetryServiceConfig.Endpoint);
                        });
                });
        }
    }
}