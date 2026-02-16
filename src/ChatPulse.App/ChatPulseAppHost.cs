using ChatPulse.IntegrationLogic;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatPulse.App
{
    public class ChatPulseAppHost : IHostedService
    {
        private readonly ObsStreamManager _streamManager;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger<ChatPulseAppHost> _logger;

        public ChatPulseAppHost(IHostApplicationLifetime appLifetime, ILogger<ChatPulseAppHost> logger, ObsStreamManager streamManager)
        {
            _appLifetime = appLifetime;
            _logger = logger;
            _streamManager = streamManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _streamManager.ConnectToObs(cancellationToken);
            await Task.Delay(1000); // Wait for a moment to ensure the connection is established before stopping the application. This is just for demonstration purposes and should be replaced with actual logic to keep the application running as needed.
            var response = await _streamManager.IsStreaming(cancellationToken);
            _logger.LogInformation("Is streaming: {IsStreaming}", response);
            _appLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
