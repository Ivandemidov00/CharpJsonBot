namespace CSharpJson.Application.Settings;

public record TelegramSettings
{
    public string BaseAddressTelegram { get; } = "https://api.telegram.org/";
    public string Token { get; init; }
    public string Url { get; init; }
}