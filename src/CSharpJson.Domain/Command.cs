namespace CSharpJson.Domain;

public struct Command
{
    public static string SetWebHook { get; } = "setWebhook?url=";
    public static string SendMessage { get; } = "sendMessage";
}