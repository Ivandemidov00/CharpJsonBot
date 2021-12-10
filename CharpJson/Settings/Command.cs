namespace CharpJson.Settings
{
    public record Command
    {
        public String SetWebHook { get; init; }
        public String GetMe { get; init; }
        public String SendMessage { get; init; }
    }
    
}