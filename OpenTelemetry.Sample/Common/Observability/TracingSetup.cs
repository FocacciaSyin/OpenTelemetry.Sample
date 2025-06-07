using Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Common.Observability;

public static class TracingSetup
{
    public static IServiceCollection AddTracing(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
        var assembly_serviceName = apiSettings.OTLP_ServiceName;
        var assembly_serviceVersion = apiSettings.OTLP_Version;

        //[基本設定]
        var otel = builder.Services.AddOpenTelemetry();

        //[Tracing]
        otel.WithTracing(tracing =>
        {
            tracing.AddSource(assembly_serviceName);
            tracing.SetResourceBuilder(ResourceBuilder
                .CreateDefault()
                .AddService(assembly_serviceName,
                    serviceVersion: assembly_serviceVersion));

            tracing.AddAspNetCoreInstrumentation(options =>
            {
                options.Filter = (httpContext) =>
                {
                    //排除一些不需要觀察的路徑
                    return !(httpContext.Request.Path == "/health" ||
                             httpContext.Request.Path == "/" ||
                             httpContext.Request.Path == "/metrics"); //排除 localhost:3100
                };
            });
            tracing.AddHttpClientInstrumentation(options => { });
            tracing.AddEntityFrameworkCoreInstrumentation(options => { options.SetDbStatementForText = true; });

            if (string.IsNullOrEmpty(apiSettings.OTLP_Tracing_URL) == false)
            {
                //匯出 Tracing 資料到 Tempo
                tracing.AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = new Uri(apiSettings.OTLP_Tracing_URL); });
            }
            else
            {
                tracing.AddConsoleExporter();
            }
        });

        return services;
    }
}
