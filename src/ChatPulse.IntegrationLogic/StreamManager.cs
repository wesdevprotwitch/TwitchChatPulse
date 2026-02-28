using ChatPulse.BusinessLogic;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.Logging;

namespace ChatPulse.IntegrationLogic
{
    public class StreamManager
    {
        private readonly ILogger<StreamManager> _logger;
        private readonly ObsWebSocketClient _client;

        public StreamManager(ILogger<StreamManager> logger, ObsWebSocketClient client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<bool> IsStreaming(CancellationToken cancellationToken = default)
        {
            await _client.SendRequestAsync("GetStreamingStatus", cancellationToken);
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
