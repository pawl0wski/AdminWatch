using AdminWatchClient.ServerConnector.Actions;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public static class ServerConnectorMethodExecutorBinder
{
    
    public static void BindAllActions(ref HubConnection connection)
    {
        BindShutdown(ref connection);
    }

    private static void BindShutdown(ref HubConnection connection)
    {
        connection.On("ShutDown", new ShutdownMethodExecutor().Execute);
    }
}