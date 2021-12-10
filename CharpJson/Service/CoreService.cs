using CharpJson.Settings;
using CharpToJson.Service;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CharpJson.Service
{
    public class CoreService : ICoreService
    {
        private readonly CharpJson.Settings.Telegram _telegramSettings;
        private readonly Command _command;

        public CoreService(IOptionsMonitor<CharpJson.Settings.Telegram> telegram,IOptionsMonitor<Command> command)
            =>(_telegramSettings,_command) = (telegram.CurrentValue,command.CurrentValue);

        public async Task<String> ExecuteAsync(Object update)
        {
            var updateDto = JsonConvert.DeserializeObject<Update>(update.ToString());
           return await (_telegramSettings.Token + _command.SendMessage)
                .SetQueryParams(
                    new { chat_id = updateDto.Message?.Chat.Id, text = "HI, " + updateDto.Message?.Chat.FirstName })
                .GetStringAsync();
        }

        public async Task<String> Start()
            => await (_telegramSettings.Token + _command.SetWebHook + _telegramSettings.Url)
                .GetStringAsync();
    }
}