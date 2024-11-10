using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;
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
        // 反射取得服務相關的類別庫名稱
        var assembly_serviceName = Assembly.GetEntryAssembly()?.GetName().Name;
        var assembly_serviceVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        //[基本設定]
        var meterName = new Meter(assembly_serviceName, assembly_serviceVersion);

        var otel = builder.Services.AddOpenTelemetry();

        otel.ConfigureResource(resource => resource.AddService(serviceName: assembly_serviceName));

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
        otel.WithTracing(tracing =>
        {
            tracing.SetResourceBuilder(
                ResourceBuilder
                    .CreateDefault()
                    .AddService(assembly_serviceName, serviceVersion: assembly_serviceVersion));

            tracing.AddAspNetCoreInstrumentation(options =>
            {
                options.Filter = (httpContext) =>
                {
                    //排除一些不需要觀察的路徑
                    return !(httpContext.Request.Path == "/health" ||
                             httpContext.Request.Path == "/" ||
                             httpContext.Request.Path == "/metrics");
                };
            });
            
            tracing.AddEntityFrameworkCoreInstrumentation(options => { options.SetDbStatementForText = true; });
            tracing.AddHttpClientInstrumentation(options => { });
            tracing.AddSource(assembly_serviceName);

            var endPointURL = builder.Configuration["OTLP_ENDPOINT_URL"];
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
