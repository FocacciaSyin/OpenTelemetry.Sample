using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Debugging;

namespace Common.Observability;

public static class LoggingSetup
{
    public static IServiceCollection AddLogging(this IServiceCollection services, WebApplicationBuilder builder)
    {
        SelfLog.Enable(Console.Error);
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((ctx, cfg) =>
            cfg.ReadFrom.Configuration(ctx.Configuration));
        
        return services;
    }
}
