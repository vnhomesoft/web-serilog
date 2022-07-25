using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger(); // <-- Change this line! (require package Serilog.AspNetCore)
// Add services toto the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Implement two-stage initialization
// Require packages: Microsoft.Extensions.Hosting, Serilog.Extensions.Hosting
builder.Host.ConfigureDefaults(args)
    .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.File("log/log-.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Console());

            
var app = builder.Build();

// Configure the HTTP request pipeline.
// Use request loging (optional)
app.UseSerilogRequestLogging(); // <-- Add this line (first, the middleware will not time or log components that appear before it in the pipeline)

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
