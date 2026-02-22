using ChatPulse.IntegrationLogic;
using ChatPulse.IntegrationLogic.Communication;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ChatPulse.BusinessLogic;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using ChatPulse.BusinessLogic.ObsMessages;

namespace ChatPulse.App
{
    public class Program
    {
        private static IHostBuilder CreateConsoleAppHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((contex, services) =>
                {
                    services.AddLogging(config =>
                    {
                        config.AddConsole();
                    });

                    services.AddSingleton(sp =>
                    {
                        var config = new ObsWebSocketClientConfig();
                        var configuration = sp.GetRequiredService<IConfiguration>();

                        // Bind from appsettings.json
                        configuration.GetSection("ObsWebSocketClient").Bind(config);

                        // Override with simple env var
                        config.Password = configuration["ObsPassword"] ?? config.Password;

                        return config;
                    });

                    services.AddSingleton<ConcurrentQueue<EventMessage>>();

                    services.AddSingleton<ObsWebSocketClient>()
                            .AddSingleton<ObsHandler>()
                            .AddSingleton<StreamManager>()
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
