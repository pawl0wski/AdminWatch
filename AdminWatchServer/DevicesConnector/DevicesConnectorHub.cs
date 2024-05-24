using AdminWatchServer.DevicesConnector.Actions;
using AdminWatchServer.DevicesConnector.Requests;
using AdminWatchServer.Services.Devices;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector;

public class DevicesConnectorHub(AdminWatchContext context, IDevicesRepository devicesRepository) : Hub
{
    private readonly ConnectToServerHubMethodExecutor _connectToServerHub = new(context);

    private readonly UpdateDeviceInfoHubMethodExecutor _updateDeviceInfo = new(devicesRepository, context);

    private readonly AddDeviceCpuUtilization _addDeviceCpuUtilization = new(devicesRepository, context);
    
    private readonly AddDeviceMemoryOccupyMethodExecutor _addDeviceMemoryOccupy = new(devicesRepository, context); 
    
    public Task<string> ConnectToServer(ConnectToServerRequest req) 
        => _connectToServerHub.Execute(Clients.Caller, Context, req);

    public Task<bool> UpdateDeviceInfo(UpdateDeviceInfoRequest req)
        => _updateDeviceInfo.Execute(Clients.Caller, Context, req);

    public Task<bool> AddDeviceCpuUtilization(AddDeviceCpuUtilizationRequest req)
        => _addDeviceCpuUtilization.Execute(Clients.Caller, Context, req);
    
    public Task<bool> AddDeviceMemoryOccupy(AddDeviceMemoryOccupyRequest req)
        => _addDeviceMemoryOccupy.Execute(Clients.Caller, Context, req);

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var device = context.Devices.First(d => d.ConnectionId == Context.ConnectionId);
        
        device.Disconnect();
        await context.SaveChangesAsync();
        
        await base.OnDisconnectedAsync(exception);
    }
}