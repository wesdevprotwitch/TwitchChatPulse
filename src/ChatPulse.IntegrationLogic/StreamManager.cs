using ChatPulse.BusinessLogic;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.Logging;

namespace ChatPulse.IntegrationLogic
{
    public class StreamManager
    {
        private readonly ILogger<StreamManager> _logger;

        ObsOrchestrator _orchestrator;

        public StreamManager(ILogger<StreamManager> logger, ObsOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        public async Task<bool> IsStreaming(CancellationToken cancellationToken = default)
        {
            var _client = _orchestrator.Client;

            await _orchestrator.Ready; // Ensure OBS connection is ready

            // Use an ENUM???
            string eventName = "GetStreamStatus";

            await _client.SendAsync(eventName, cancellationToken);

            // TODO: Call event reader...
            var response = await _client.ReceiveEventAsync(eventName);
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
