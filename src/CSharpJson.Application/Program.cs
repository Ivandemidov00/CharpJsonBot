using CSharpJson.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .WriteTo
    .Console()
    .CreateLogger();

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddInfrastructure();
services.AddSwaggerGen();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
