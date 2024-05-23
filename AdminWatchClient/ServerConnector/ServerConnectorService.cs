using AdminWatchClient.ServerConnector.Exceptions;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public class ServerConnectorService : IServerConnectorService
{
    private ConnectionState state = new();
    
    private readonly HubConnection _connection;

    public ServerConnectorService()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5025/DevicesConnector")
            .Build();

        ServerConnectorMethodExecutorBinder.BindAllActions(ref _connection, state);
    }

    public async Task Connect()
    {
        try
        {
            await _connection.StartAsync();
        }
        catch (Exception)
        {
            throw new ConnectToServerFailException("Can't connect to AdminWatch server.");
        }

        state.Id = await _connection.InvokeAsync<string>("ConnectToServer", "");
    }

}