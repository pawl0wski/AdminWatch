using AdminWatchServer.DevicesConnector.Hubs;
using AdminWatchServer.DevicesConnector.Obtainers;
using AdminWatchServer.Models;
using AdminWatchServer.Services.Devices;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer;

public class DeviceWatchWorker(
    ILogger<DeviceWatchWorker> logger,
    IServiceScopeFactory scopeFactory,
    IHubContext<DevicesConnectorHub> hub) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Updating devices...");
            
            await using var scope = scopeFactory.CreateAsyncScope();
            
            var devicesRepository = GetDevicesRepository(scope);
            var dbContext = GetDatabaseContext(scope);
            
            foreach (var device in devicesRepository.GetAllConnectedDevices())
            {
                try
                {
                    var deviceClient = hub.Clients.Client(device.ConnectionId!);

                    await UpdateDevice(device, deviceClient);

                    logger.LogTrace($"Updated device with Id: {device.Id}");
                }
                catch (IOException)
                {
                    logger.LogError($"Device Id: {device.Id} disconnected while data fetching.");
                    device.Disconnect();
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task UpdateDevice(Device device, ISingleClientProxy deviceClient)
    {
        await DeviceObtainer.UpdateFromClient(deviceClient, device);

        await AddNewCpuUtilization(device, deviceClient);

        await AddNewMemoryOccupy(device, deviceClient);
    }

    private async Task AddNewMemoryOccupy(Device device, ISingleClientProxy deviceClient)
    {
        var currentMemoryOccupy =
            await DeviceMemoryOccupyObtainer.GetFromClient(deviceClient);
        device.MemoryOccupies.Add(currentMemoryOccupy);
    }

    private async Task AddNewCpuUtilization(Device device, ISingleClientProxy deviceClient)
    {
        var currentCpuUtilization =
            await DeviceCpuUtilizationObtainer.GetFromClient(deviceClient);
        device.CpuUtilizations.Add(currentCpuUtilization);
    }

    private AdminWatchContext GetDatabaseContext(AsyncServiceScope scope)
        => scope.ServiceProvider.GetRequiredService<AdminWatchContext>();

    private IDevicesRepository GetDevicesRepository(AsyncServiceScope scope) =>
        scope.ServiceProvider.GetRequiredService<IDevicesRepository>();
}