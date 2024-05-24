using AdminWatchClient.ServerConnector.Exceptions;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public class ServerConnector : IServerConnector
{
    private ConnectionState state = new();
    
    private readonly HubConnection _connection;

    public ServerConnector(IConfiguration config, IHardwareService hardwareService)
    {
        var serverIpAndPort = config.GetValue<string>("ip");
        _connection = new HubConnectionBuilder()
            .WithUrl($"http://{serverIpAndPort}/DevicesConnector")
            .Build();

        ServerConnectorMethodExecutorBinder.BindAllActions(ref _connection, hardwareService);
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