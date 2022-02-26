using CSharpJson.Application.Settings;
using CSharpJson.Application.Verification;
using CSharpJson.Domain;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CSharpJson.Application.Core
{

    public class CoreService : ICoreService
    {
        private const string InvalidMessage = "invalid message";
        private readonly Command _command;
        private TelegramSettings _telegramSettings;
        private readonly IIdentificationService _identificationService;

        public CoreService(IOptionsMonitor<TelegramSettings> optionsMonitor, IOptionsMonitor<Command> command, IIdentificationService identificationService)
        {
            _identificationService = identificationService;
            _command = command.CurrentValue;
            _telegramSettings = optionsMonitor.CurrentValue;
            optionsMonitor.OnChange(_ => _telegramSettings = _);
        }

        public async Task<string> ExecuteAsync(object update)
        {
            var updateDto = JsonConvert.DeserializeObject<Update>(update.ToString());
            var typeMessage =  await _identificationService.CheckType(updateDto.Message?.Text);
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

        public async Task<IFlurlResponse> SetWebHook()
            => await (_telegramSettings.Token + _command.SetWebHook + _telegramSettings.Url)
                .GetAsync();
    }
}