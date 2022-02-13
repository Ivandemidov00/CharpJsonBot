using CharpJson.Service.Interface;
using CharpJson.Settings;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CharpJson.Service.Implementation
{

    public class CoreService : ICoreService
    {
        private const string InvalidMessage = "invalid message";
        private readonly Command _command;
        private readonly Settings.Telegram _telegramSettings;
        private readonly IIdentificationService _identificationService;

        public CoreService(IOptionsMonitor<Settings.Telegram> telegram, IOptionsMonitor<Command> command, IIdentificationService identificationService)
        {
            _telegramSettings = telegram.CurrentValue;
            _command = command.CurrentValue;
            _identificationService = identificationService;
        }

        public async Task<string> ExecuteAsync(object update)
        {
            var updateDto = JsonConvert.DeserializeObject<Update>(update.ToString());
            var typeMessage =  await _identificationService.CheckType(updateDto.Message.Text);
            var reply = typeMessage.ToString();
            return await (_telegramSettings.Token + _command.SendMessage)
                .SetQueryParams(
                    new
                    {
                        chat_id = updateDto.Message?.Chat.Id, text = reply,
                        parse_mode = "MarkdownV2"
                    })
                .GetStringAsync();
        }

        public async Task<string> Start()
            => await (_telegramSettings.Token + _command.SetWebHook + _telegramSettings.Url)
                .GetStringAsync();
    }
}