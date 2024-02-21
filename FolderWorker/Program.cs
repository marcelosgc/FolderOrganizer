using FolderWorker;

IHostBuilder host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<FolderOptions>(configuration.GetSection(nameof(FolderOptions)));

        services.AddHostedService<Worker>();
    });
host.Start();

namespace FolderWorker
{
    public class FolderOptions
    {
        public string Directory { get; set; }
        public string Worker { get; set; }
    }
}