namespace ChatPulse.BusinessLogic
{
    public class ObsWebSocketClientConfig
    {
        public string Host { get; } = "localhost";
        public int Port { get; } = 4455; // PORT WILL CHANGE IN THE FUTURE, THIS IS JUST THE DEFAULT ONE FOR OBS
        public string Path { get; } = "chatpulse";
        // Needs to be injected in the future if authentication is required, but for now, OBS doesn't require it by default, so we can leave it out.
        public string Password { get; } = ""; // EMPTY for NOW
        public Uri Uri => new Uri($"ws://{Host}:{Port}");
        public Uri UriPath => new Uri($"ws://{Host}:{Port}/{Path}".TrimEnd('/'));
    }
}
