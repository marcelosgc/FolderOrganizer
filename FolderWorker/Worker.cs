using FolderOrganizer.Jobs;
using Microsoft.Extensions.Options;


namespace FolderWorker;

public class Worker : BackgroundService
{
    
    private readonly IOptions<FolderOptions> _folder;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, IOptions<FolderOptions> folder)
    {
        _logger = logger;
        _folder = folder;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartEventLog();
        while (!stoppingToken.IsCancellationRequested)
        {
            OrganizeFolderByTypeJob.Execute(_folder.Value.Directory);
            await Task.Delay(1000);
        }

        StopEventLog();
    }

    private void StartEventLog()
    {
        
      
        
    }

    private void StopEventLog()
    {   
        

    }
}