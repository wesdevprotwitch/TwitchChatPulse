namespace ChatPulse.BusinessLogic
{
    public class ObsWebSocketClientConfig
    {
        public string Host { get; } = "localhost";
        public int Port { get; } = 4455; // PORT WILL CHANGE IN THE FUTURE, THIS IS JUST THE DEFAULT ONE FOR OBS
        public string Path { get; } = "chatpulse";
         public string Password { get; set; } = string.Empty;
        public Uri Uri => new Uri($"ws://{Host}:{Port}");
        public Uri UriPath => new Uri($"ws://{Host}:{Port}/{Path}".TrimEnd('/'));
    }
}
