using ChatPulse.IntegrationLogic;
using ChatPulse.IntegrationLogic.Communication;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ChatPulse.BusinessLogic;

namespace ChatPulse.App
{
    public class Program
    {
        private static IHostBuilder CreateConsoleAppHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddLogging(config =>
                    {
                        config.AddConsole();
                    });

                    services.AddOptions<ObsWebSocketClientConfig>()
                            .BindConfiguration("ObsWebSocketClient");

                    services.AddSingleton<ObsWebSocketClient>()
                            .AddSingleton<ObsHandler>()
                            .AddSingleton<ObsStreamManager>()
                            .AddHostedService<ChatPulseAppHost>();
                });
        }


        private static async Task Main(string[] args)
        {
            using var host = CreateConsoleAppHost(args).Build();
            await host.RunAsync();
            return;
        }
    }
}
