using AdminWatchClient.Services.ServerConnector;

namespace AdminWatchClient;

public class Worker(ILogger<Worker> logger, IServerConnectorService serverConnector) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
        
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}