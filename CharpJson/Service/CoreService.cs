using CharpJson.Settings;
using CharpToJson.Service;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CharpJson.Service;

public class CoreService : ICoreService
{
    private readonly Command _command;
    private readonly Settings.Telegram _telegramSettings;

    public CoreService(IOptionsMonitor<Settings.Telegram> telegram, IOptionsMonitor<Command> command)
        => (_telegramSettings, _command) = (telegram.CurrentValue, command.CurrentValue);

    public async Task<string> ExecuteAsync(object update)
    {
        var updateDto = JsonConvert.DeserializeObject<Update>(update.ToString());
        return await (_telegramSettings.Token + _command.SendMessage)
            .SetQueryParams(
                new
                {
                    chat_id = updateDto.Message?.Chat.Id, text = $"`HI, {updateDto.Message?.Chat.FirstName}`",
                    parse_mode = "MarkdownV2"
                })
            .GetStringAsync();
    }

    public async Task<string> Start()
        => await (_telegramSettings.Token + _command.SetWebHook + _telegramSettings.Url)
            .GetStringAsync();
}