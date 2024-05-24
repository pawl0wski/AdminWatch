using AdminWatchClient.ServerConnector.Requests;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector.Senders;

public class ConnectToBaseServerSender(ConnectionState connectionState) 
    : BaseServerConnectorSender<ConnectToServerRequest, Guid>(connectionState)
{
    protected override async Task<Guid> SendRequest(HubConnection connection, ConnectToServerRequest request)
    {
        return await connection.InvokeAsync<Guid>("ConnectToServer", request);
    }

    protected override ConnectToServerRequest PrepareRequest()
    {
        return new ConnectToServerRequest
        {
            DeviceId = GetDeviceId(),
            Name = Environment.MachineName
        };
    }
}