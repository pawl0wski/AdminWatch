using AdminWatchClient.ServerConnector;

namespace AdminWatchClient;

public class Worker(ILogger<Worker> logger, IServerConnector serverConnector) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await serverConnector.UpdateDeviceInfo();
        logger.LogInformation("Sent information about device to server.");
        while (!stoppingToken.IsCancellationRequested)
        {
            await serverConnector.AddDeviceCpuUtilization();
            await serverConnector.AddDeviceMemoryOccupy();
            logger.LogInformation("Sent information about CPU and RAM utilization");
            
            await Task.Delay(3000, stoppingToken);
        }
    }
}