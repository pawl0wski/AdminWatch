using AdminWatchServer.DevicesConnector.Hubs.Actions;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Hubs;

public class DevicesConnectorHub(AdminWatchContext context) : Hub
{
    private readonly ConnectToServerMethodExecutor _connectToServer = new(context);
    public Task<string> ConnectToServer(string stringDeviceId) 
        => _connectToServer.Execute(Clients.Caller, stringDeviceId);
}