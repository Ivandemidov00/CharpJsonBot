namespace CharpJson.Settings;

public record Telegram
{
    public string Token { get; init; }
    public string Url { get; init; }
}