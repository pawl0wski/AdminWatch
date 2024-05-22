using AdminWatchClient.Services.ServerConnector.Actions;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.Services.ServerConnector;

public static class ServerConnectorActionBinder
{
    
    public static void BindAllActions(ref HubConnection connection, ConnectionState state)
    {
        BindShutdown(ref connection);
        BindGetName(ref connection);
        BindGetOperatingSystem(ref connection);
        BindGetCpuName(ref connection);
    }

    private static void BindGetCpuName(ref HubConnection connection)
    {
        connection.On("GetCpuName", new GetCpuName().Execute);
    }

    private static void BindGetOperatingSystem(ref HubConnection connection)
    {
        connection.On("GetOperatingSystem", new GetOperatingSystemAction().Execute);
    }

    private static void BindShutdown(ref HubConnection connection)
    {
        connection.On("ShutDown", new ShutdownAction().Execute);
    }

    private static void BindGetName(ref HubConnection connection)
    {
        connection.On("GetDeviceName", new GetDeviceNameAction().Execute);
    }
}