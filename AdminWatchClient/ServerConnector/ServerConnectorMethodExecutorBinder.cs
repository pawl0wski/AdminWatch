using AdminWatchClient.ServerConnector.Actions;
using AdminWatchClient.ServerConnector.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public static class ServerConnectorMethodExecutorBinder
{
    
    public static void BindAllActions(ref HubConnection connection)
    {
        BindShutdown(ref connection);
        BindGetProcesses(ref connection);
        BindKillProcess(ref connection);
    }

    private static void BindKillProcess(ref HubConnection connection)
    {
        connection.On("KillProcess",
            (DeviceProcess process) => 
            new KillProcessMethodExecutor().Execute(process));
    }

    private static void BindGetProcesses(ref HubConnection connection)
    {
        connection.On("GetProcesses",
            (object req) => 
            new GetProcessesMethodExecutor().Execute(req));
    }

    private static void BindShutdown(ref HubConnection connection)
    {
        connection.On("ShutDown",
            (object req) => 
            new ShutdownMethodExecutor().Execute(req));
    }
}