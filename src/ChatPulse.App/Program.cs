using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatPulse.App
{
    public class Program
    {
        private static IHostBuilder CreateConsoleAppHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<ChatPulseAppHost>();
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