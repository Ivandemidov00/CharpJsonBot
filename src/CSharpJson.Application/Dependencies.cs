using CSharpJson.Application.Core;
using CSharpJson.Application.Settings;
using CSharpJson.Application.Verification;
using CSharpJson.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpJson.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<ICoreService, CoreService>();
            serviceCollection.AddSingleton<IIdentificationService, IdentificationService>();
            serviceCollection.Configure<Command>(configuration.GetSection(nameof(Command)));
            serviceCollection.Configure<TelegramSettings>(configuration.GetSection(nameof(TelegramSettings)));

            return serviceCollection;
        }
        
    }
}