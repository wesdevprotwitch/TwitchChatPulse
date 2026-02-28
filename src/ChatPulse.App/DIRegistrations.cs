using ChatPulse.BusinessLogic;
using ChatPulse.BusinessLogic.ObsMessages;
using ChatPulse.IntegrationLogic;
using ChatPulse.IntegrationLogic.Communication.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ChatPulse.App
{
    public static class DIRegistrations
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
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
                    .AddSingleton<StreamManager>()
                    .AddHostedService<ChatPulseAppHost>();
            return services;
        }
    }
}
