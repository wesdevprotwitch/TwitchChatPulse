using ChatPulse.BusinessLogic;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.Logging;

namespace ChatPulse.IntegrationLogic
{
    public class ObsStreamManager
    {
        private readonly ILogger<ObsStreamManager> _logger;
        // NOTE: Use client directly for now...
        //private readonly ObsHandler _obs;

        private readonly ObsWebSocketClient _client;

        public ObsStreamManager(ILogger<ObsStreamManager> logger, ObsWebSocketClient client)//ObsHandler obs)
        {
            _client = client;
            _logger = logger;
            //_obs = obs;
        }

        public async Task ConnectToObs(CancellationToken token = default)
        {
            await _client.ConnectAsync();
            return;
        }

        public async Task<bool> IsStreaming(CancellationToken cancellationToken = default)
        {
            await _client.SendAsync("GetStreamStatus", cancellationToken);
            var response = await _client.ReceiveMessageAsync(cancellationToken);
            var response2 = await _client.ReceiveMessageAsync(cancellationToken);
            return response != null;
        }

        public async Task<IReadOnlyList<string>> GetSceneList()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCurrentSceneCollection()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCurrentScene()
        {
            throw new NotImplementedException();
        }
    }
}
