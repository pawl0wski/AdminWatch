using AdminWatchClient.ServerConnector;

namespace AdminWatchClient;

public class Worker(ILogger<Worker> logger, IServerConnector serverConnector) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}