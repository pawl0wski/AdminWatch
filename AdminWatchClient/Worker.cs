using AdminWatchClient.ServerConnector;
using AdminWatchClient.ServerConnector.Exceptions;

namespace AdminWatchClient;

public class Worker(ILogger<Worker> logger, IServerConnector serverConnector) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var deviceInfoUpdateIndicator = 0;
        await ConnectServerService();
        logger.LogInformation("Connected to Server.");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await TryToUpdateDeviceInfo(ref deviceInfoUpdateIndicator);
            
            await serverConnector.AddDeviceCpuUtilization();
            await serverConnector.AddDeviceMemoryOccupy();
            logger.LogInformation("Sent information about CPU and RAM utilization.");
            
            deviceInfoUpdateIndicator++;
            await Task.Delay(3000, stoppingToken);
        }
    }

    private Task TryToUpdateDeviceInfo(ref int deviceInfoUpdateIndicator)
    {
        deviceInfoUpdateIndicator %= 10;

        if (deviceInfoUpdateIndicator == 0)
        {
            logger.LogInformation("Updating device info.");
            return serverConnector.UpdateDeviceInfo();
        }
        return Task.CompletedTask;
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