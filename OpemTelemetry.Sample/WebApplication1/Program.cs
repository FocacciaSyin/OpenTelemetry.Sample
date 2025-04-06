using Common.Observability;
using Common.Settings;
using Microsoft.Extensions.Options;
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

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var apiSettings = app.Services.GetRequiredService<IOptions<ApiSettings>>().Value;
logger.LogInformation("[Woody] Log {BaseUrl}", apiSettings.BaseUrl);

app.MapPrometheusScrapingEndpoint();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
