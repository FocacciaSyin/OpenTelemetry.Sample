using System.Diagnostics.Metrics;
using System.Reflection;
using Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Common.Observability;

public static class MetricsSetup
{
    public static IServiceCollection AddMetrics(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
        var assembly_serviceName = apiSettings.OTLP_ServiceName;
        var assembly_serviceVersion = apiSettings.OTLP_Version;

        //[基本設定]
        var meterName = new Meter(assembly_serviceName, assembly_serviceVersion);

        var otel = builder.Services.AddOpenTelemetry();

        otel.ConfigureResource(resource =>
            resource.AddService(serviceName: assembly_serviceName));

        //[Metrics]
        otel.WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter(meterName.Name)
            // Metrics provides by ASP.NET Core in .NET 8
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddPrometheusExporter()); // 匯出 metrics 到 Prometheus

        return services;
    }
}
