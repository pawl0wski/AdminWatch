using AdminWatchClient.ServerConnector.Requests;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector.Senders;

public abstract class BaseServerConnectorSender<TRequest, TResponse>(ConnectionState connectionState) 
    where TRequest : HubRequest
{
    public async Task<TResponse> Execute(HubConnection connection)
    {
        var request = PrepareRequest();
        return await SendRequest(connection, request);
    }
    
    protected abstract Task<TResponse> SendRequest(HubConnection connection, TRequest request);
    
    protected abstract TRequest PrepareRequest();

    protected Guid GetDeviceId()
        => connectionState.DeviceId;
}

