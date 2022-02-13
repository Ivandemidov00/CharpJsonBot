using Telegram.Bot.Types;

namespace CharpJson.Service.Interface
{
    public interface IIdentificationService
    {
        public Task<TypeMessage> CheckType(string message);
    }
}