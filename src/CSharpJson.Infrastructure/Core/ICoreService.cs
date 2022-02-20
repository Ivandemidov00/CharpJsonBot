using Flurl.Http;

namespace CSharpJson.Infrastructure.Core
{

    public interface ICoreService
    {
        public Task<string> ExecuteAsync(object update);

        public Task<IFlurlResponse> SetWebHook();
    }
}