using Microsoft.Extensions.Hosting;

namespace ChatPulse.App
{
    public class ChatPulseAppHost : IHostedService
    {
        public ChatPulseAppHost()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
