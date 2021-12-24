namespace CharpJson.Settings;

public record Command
{
    public string SetWebHook { get; init; }
    public string GetMe { get; init; }
    public string SendMessage { get; init; }
}