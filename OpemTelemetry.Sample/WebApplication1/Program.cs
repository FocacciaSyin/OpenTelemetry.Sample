using Common.Observability;
using Common.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Serilog;
using WebApplication1.Infrastructure.Helper;
using WebApplication1.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//[OpenTelemetry-基本設定]
builder.Services.AddTracing(builder)
    .AddMetrics(builder)
    .AddLogging(builder);

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ApiHelper>();
builder.Services.AddHttpClient("Default", (serviceProvider, client) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(apiSettings.BaseUrl);
});

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP [{StatusCode}] {RequestMethod} {RequestPath} {QueryString} in {Elapsed:0.0000} ms";
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("QueryString", httpContext.Request.QueryString.Value);
    };
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
