using Common.Observability;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication2.Repository;
using WebApplication2.Repository.Product;
using WebApplication2.Repository.User;
using WebApplication2.Service.Product;
using WebApplication2.Service.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//[OpenTelemetry-基本設定]
// builder.Services.AddOpenTelemetrySettings(builder);
builder.Services.AddTracing(builder)
    .AddMetrics(builder);
    // .AddLogging(builder);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
