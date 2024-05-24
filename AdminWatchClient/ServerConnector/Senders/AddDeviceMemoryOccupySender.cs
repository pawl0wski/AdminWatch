using AdminWatchClient.ServerConnector.Requests;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector.Senders;

public class AddDeviceMemoryOccupySender(ConnectionState connectionState, IHardwareService hardwareService) 
    : BaseServerConnectorSender<AddDeviceMemoryOccupyRequest, bool>(connectionState)
{
    protected override async Task<bool> SendRequest(HubConnection connection, AddDeviceMemoryOccupyRequest request)
    {
        return await connection.InvokeAsync<bool>("AddDeviceMemoryOccupy", request);
    }

    protected override AddDeviceMemoryOccupyRequest PrepareRequest()
        => new()
        {
            DeviceId = GetDeviceId(),
            OccupiedMemory = hardwareService.GetOccupiedMemory()
        };
}