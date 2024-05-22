using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.Services;

public class ServerService : IServerService
{
    public Action? OnShutdown { get; set; }

    private HubConnection _connection;
    
    public ServerService()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5025/hub/devices")
            .Build();

        _connection.On("ShutDown", OnShutdown ?? NotSetAction);
    }

    public async Task Connect() => 
        await _connection.StartAsync();

    private void NotSetAction()
    {
        throw new NotImplementedException("Action is not set.");
    }
    
}