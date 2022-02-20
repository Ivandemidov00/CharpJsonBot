using CSharpJson.Infrastructure.Core;
using CSharpJson.Infrastructure.Verification;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpJson.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICoreService, CoreService>();
            serviceCollection.AddSingleton<IIdentificationService, IdentificationService>();
            return serviceCollection;
        }
        
    }
}