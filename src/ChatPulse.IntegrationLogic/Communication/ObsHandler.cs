using ChatPulse.IntegrationLogic.Communication.WebSockets;

namespace ChatPulse.IntegrationLogic.Communication
{
    public class ObsHandler
    {
        private readonly ObsWebSocketClient _client;

        public ObsHandler(ObsWebSocketClient client)
        {
            _client = client;
            //_client.ConnectAsync().GetAwaiter().GetResult();
        }


        public async Task StartConnection()
        {
            await _client.ConnectAsync();
        }

        public async Task StopConnection()
        {
            // The Dispose method of ObsWebSocketClient will handle closing the connection, so we just need to dispose of the client here.
            // Or we may want to explicitly close the connection before disposing of the client, depending on the desired behavior. For now, we'll just dispose of the client.
            _client.Dispose();
        }
    }
}
