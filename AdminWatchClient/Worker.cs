using AdminWatchClient.ServerConnector;
using AdminWatchClient.ServerConnector.Exceptions;

namespace AdminWatchClient;

public class Worker(ILogger<Worker> logger, IServerConnector serverConnector) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectServerService();
        logger.LogInformation("Connected to Server.");
        
        await serverConnector.UpdateDeviceInfo();
        logger.LogInformation("Sent information about device to server.");
        while (!stoppingToken.IsCancellationRequested)
        {
            await serverConnector.AddDeviceCpuUtilization();
            await serverConnector.AddDeviceMemoryOccupy();
            logger.LogInformation("Sent information about CPU and RAM utilization.");
            
            await Task.Delay(3000, stoppingToken);
        }
    }
    
    private async Task ConnectServerService()
    {
        try
        {
            await serverConnector.Connect();
        }
        catch (ConnectToServerFailException)
        {
            await Console.Error.WriteLineAsync("Can't connect to AdminWatch Server.\nCheck provided IP Address.");
            Environment.Exit(1);
        }
    }
}