using Microsoft.Extensions.Hosting;

namespace ChatPulse.App
{
    public class Program
    {
        private static IHostBuilder CreateConsoleAppHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddAppServices();
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
