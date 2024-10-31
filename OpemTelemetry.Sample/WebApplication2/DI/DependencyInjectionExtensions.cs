using System.Diagnostics;
using System.Diagnostics.Metrics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace WebApplication2.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddOpenTelemetrySettings(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        //[基本設定]
        var serviceName = builder.Configuration["OTEL_SERVICE_NAME"];
        var meterName = new Meter(serviceName, "1.0.0");

        var otel = builder.Services.AddOpenTelemetry();

        otel.ConfigureResource(resource => resource.AddService(serviceName: serviceName));

        //[Metrics]
        otel.WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter(meterName.Name)
            // Metrics provides by ASP.NET Core in .NET 8
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddPrometheusExporter()); // 匯出 metrics 到 Prometheus

        //[Tracing]
        var endPointURL = builder.Configuration["OTLP_ENDPOINT_URL"];
        var activitySource = new ActivitySource(serviceName);
        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = (httpContext) =>
                    {
                        //排除一些不需要觀察的路徑
                        return !(httpContext.Request.Path == "/health" ||
                                 httpContext.Request.Path == "/" ||
                                 httpContext.Request.Path == "/metrics");
                    };
                })
                .AddEntityFrameworkCoreInstrumentation(options => { options.SetDbStatementForText = true; })
                .AddHttpClientInstrumentation(options => { })
                .AddSource(activitySource.Name);

            if (endPointURL != null)
            {
                //匯出 Tracing 資料到 Tempo
                tracing.AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = new Uri(endPointURL); });
            }
            else
            {
                tracing.AddConsoleExporter();
            }
        });

        return services;
    }
}
