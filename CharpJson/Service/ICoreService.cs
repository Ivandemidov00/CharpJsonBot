
namespace CharpToJson.Service
{
    public interface ICoreService
    {
        public Task<String> ExecuteAsync(Object update);
        public Task<String> Start();
    }
}