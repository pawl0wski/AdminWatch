using AdminWatchServer.DevicesConnector;
using AdminWatchServer.DevicesConnector.Models;
using AdminWatchServer.DevicesConnector.Responses;
using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.Services.Processes;

public class ProcessesService(IHubContext<DevicesConnectorHub> devicesHub)
    : IProcessessService
{
    public async Task<List<DeviceProcess>> GetProcessesForDevice(Device device, CancellationToken? cancellationToken = null)
    {
        CheckIfDeviceIsConnected(device);
        
        var deviceClient = GetDeviceClient(device);
        var response = await deviceClient.InvokeAsync<GetProcessesRespone>("GetProcesses", null, cancellationToken ?? new CancellationToken());

        return response.Processes;
    }

    public async Task<bool> KillProcess(DeviceProcess process, Device device, CancellationToken? cancellationToken = null)
    {
        CheckIfDeviceIsConnected(device);

        var deviceClient = GetDeviceClient(device);
        
        return await deviceClient.InvokeAsync<bool>("KillProcess", process, cancellationToken ?? new CancellationToken());
    }

    private ISingleClientProxy GetDeviceClient(Device device)
        => devicesHub.Clients.Client(device.ConnectionId!);

    private void CheckIfDeviceIsConnected(Device device)
    {
        if (!device.IsConnected())
            throw new Exception("Device must be connected");
    }
}