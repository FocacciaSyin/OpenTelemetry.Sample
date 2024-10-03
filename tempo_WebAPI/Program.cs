using System.Diagnostics;
using System.Diagnostics.Metrics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

//[Opentelemetry-基本設定]
var greeterMeter = new Meter("OtPrGrYa.Example", "1.0.0");
var countGreetings = greeterMeter.CreateCounter<int>("greetings.count", description: "Counts the number of greetings");
var greeterActivitySource = new ActivitySource("OtPrGrJa.Example");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//[Opentelemetry-基本設定]
var tracingOtlpEndpoint = builder.Configuration["OTLP_ENDPOINT_URL"];
Console.WriteLine($"OtlpEndpoint:[{tracingOtlpEndpoint}]");

var otel = builder.Services.AddOpenTelemetry();

// Configure OpenTelemetry Resources with the application name
otel.ConfigureResource(resource => resource
    .AddService(serviceName: builder.Environment.ApplicationName));

// Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
otel.WithMetrics(metrics => metrics
    // Metrics provider from OpenTelemetry
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddMeter(greeterMeter.Name)
    // Metrics provides by ASP.NET Core in .NET 8
    .AddMeter("Microsoft.AspNetCore.Hosting")
    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
    .AddPrometheusExporter()); // 匯出 metrics 到 Prometheus

// Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation(options =>
    {
        options.Filter = (httpContext) =>
        {
            //排除一些不需要觀察的路徑
            return !(httpContext.Request.Path == "/health" || 
                    httpContext.Request.Path == "/metrics");
        };
    });
    tracing.AddHttpClientInstrumentation(options => { });
    tracing.AddSource(greeterActivitySource.Name);

    if (tracingOtlpEndpoint != null)
    {
        //匯出 Tracing 資料到 Tempo
        tracing.AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint); });
    }
    else
    {
        tracing.AddConsoleExporter();
    }
});


var app = builder.Build();

app.MapGet("/", SendGreeting);

//[Prometheus-基本設定]
app.MapPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task<String> SendGreeting(ILogger<Program> logger)
{
    // Create a new Activity scoped to the method
    using var activity = greeterActivitySource.StartActivity("GreeterActivity");

    // Log a message
    logger.LogInformation("Sending greeting");

    // Increment the custom counter
    countGreetings.Add(1);

    // Add a tag to the Activity
    activity?.SetTag("greeting", "Hello World!");

    return "Hello World!";
}