using AdminWatchServer.DevicesConnector.Hubs;
using AdminWatchServer.DevicesConnector.Obtainers;
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
        await using var scope = scopeFactory.CreateAsyncScope();

        var devicesRepository = GetDevicesRepository(scope);
        var dbContext = GetDatabaseContext(scope);


        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Updating devices...");
            foreach (var device in devicesRepository.GetAllConnectedDevices())
            {
                var deviceClient = hub.Clients.Client(device.ConnectionId!);

                await DeviceObtainer.UpdateFromClient(deviceClient, device);
                logger.LogInformation($"Updated {device.Id}");

                var currentCpuUtilization = await DeviceCpuUtilizationObtainer.GetFromClient(deviceClient);
                device.CpuUtilizations.Add(currentCpuUtilization);
                
                await dbContext.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private AdminWatchContext GetDatabaseContext(AsyncServiceScope scope)
        => scope.ServiceProvider.GetRequiredService<AdminWatchContext>();

    private IDevicesRepository GetDevicesRepository(AsyncServiceScope scope) =>
        scope.ServiceProvider.GetRequiredService<IDevicesRepository>();
}