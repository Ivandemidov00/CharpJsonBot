namespace CSharpJson.Application.Settings;

public record TelegramSettings
{
    public string Token { get; init; }
    public string Url { get; init; }
}