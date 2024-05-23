using AdminWatchServer.DevicesConnector.Hubs.Actions;
using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Hubs;

public class DevicesConnectorHub(AdminWatchContext context) : Hub
{
    private readonly ConnectToServerMethodExecutor _connectToServer = new(context);
    public Task<string> ConnectToServer(string stringDeviceId) 
        => _connectToServer.Execute(Clients.Caller, Context, stringDeviceId);

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var device = context.Devices.First(d => d.ConnectionId == Context.ConnectionId);
        
        device.Status = Device.DeviceStatus.Disconnected;
        device.ConnectionId = null;
        await context.SaveChangesAsync();

        await base.OnDisconnectedAsync(exception);
    }
}