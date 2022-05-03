using CSharpJson.Application.Handler;
using CSharpJson.Application.Settings;
using CSharpJson.Domain;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CSharpJson.Application.Core
{

    public class CoreService
    {
        private TelegramSettings _telegramSettings;
        private readonly IMessageHandlers _messageHandlers;

        public CoreService(IOptionsMonitor<TelegramSettings> optionsMonitor, IMessageHandlers messageHandlers)
        {
            _messageHandlers = messageHandlers;
            _telegramSettings = optionsMonitor.CurrentValue;
            optionsMonitor.OnChange(_ => _telegramSettings = _);
        }

        public async Task<string> ExecuteAsync(Update update, TypeMessage type)
        {
            var reply = update.Message?.Text == null
                ? TypeMessage.Invalid.ToString()
                : CallHandlers();
            return await (_telegramSettings.BaseAddressTelegram + _telegramSettings.Token + Command.SendMessage).SetQueryParams(new
                {
                    chat_id = update.Message?.Chat.Id, text = reply, parse_mode = ParseMode.MarkdownV2
                })
                .GetStringAsync();

            string CallHandlers()
                => type switch
                {
                    TypeMessage.Code => _messageHandlers.CodeHandler(
                        update.Message.Text),
                    TypeMessage.Json => _messageHandlers.JsonHandler(
                        update.Message.Text),
                    _ => TypeMessage.Invalid.ToString()
                };
        }

        public async Task<IFlurlResponse> SetWebHook()
            => await (_telegramSettings.BaseAddressTelegram + _telegramSettings.Token + Command.SetWebHook + _telegramSettings.Url)
                .GetAsync();
    }
}