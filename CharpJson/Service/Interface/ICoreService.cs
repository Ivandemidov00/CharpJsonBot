namespace CharpJson.Service.Interface
{

    public interface ICoreService
    {
        public Task<string> ExecuteAsync(object update);

        public Task<string> Start();
    }
}