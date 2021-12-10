using CharpJson.Service;
using CharpJson.Settings;
using CharpToJson.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CharpToJson
{
    public static class Dependency
    {
        public static IServiceCollection Add(IConfiguration configuration)
        {
            var log = new LoggerConfiguration().WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}").CreateLogger();
            var serviceCollection = new ServiceCollection();
            serviceCollection.Configure <CharpJson.Settings.Telegram>(configuration.GetSection(nameof(Telegram)));
            serviceCollection.Configure <Command>(configuration.GetSection(nameof(Command)));
            serviceCollection.AddSingleton<ICoreService, CoreService>();
            serviceCollection.AddSingleton(log);
            
            return serviceCollection;
        }
    }
}                                                      