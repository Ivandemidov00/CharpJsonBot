namespace CSharpJson.Application.Handler;

public class MessageHandlers : IMessageHandlers
{
    public string JsonHandler(string? json)
    {
        return "JsonToCode";
    }

    public string CodeHandler(string? code)
    {
        return "CodeToJson";
    }
}