using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.Hubs;

public class DeviceHub : Hub
{
    public async Task SendShutdown(Device device)
    {
        await Clients.All.SendAsync("ShutDown", device.Id);
    }
}