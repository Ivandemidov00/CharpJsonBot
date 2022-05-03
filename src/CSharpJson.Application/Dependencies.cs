using CSharpJson.Application.Core;
using CSharpJson.Application.Handler;
using CSharpJson.Application.Settings;
using CSharpJson.Application.Verification;
using CSharpJson.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpJson.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IMessageHandlers, MessageHandlers>();
            serviceCollection.AddSingleton<CoreService, CoreService>();
            serviceCollection.AddSingleton<ICoreService, CoreServiceVerificationProxy>();
            serviceCollection.AddSingleton<IIdentificationService, IdentificationService>();
            serviceCollection.Configure<TelegramSettings>(configuration.GetSection(nameof(TelegramSettings)));

            return serviceCollection;
        }
        
    }
}