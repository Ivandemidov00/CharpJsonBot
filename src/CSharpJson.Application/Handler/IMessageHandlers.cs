namespace CSharpJson.Application.Handler;

public interface IMessageHandlers
{
    public string JsonHandler(string? json);

    public string CodeHandler(string? code);
}