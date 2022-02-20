namespace CSharpJson.Domain;

public record Command
{
    public string SetWebHook { get; init; }
    public string SendMessage { get; init; }
}