using AdminWatchClient.ServerConnector.Actions;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public static class ServerConnectorMethodExecutorBinder
{
    
    public static void BindAllActions(ref HubConnection connection, ConnectionState state)
    {
        BindShutdown(ref connection);
        BindGetName(ref connection);
        BindGetOperatingSystem(ref connection);
        BindGetCpuName(ref connection);
        BindGetLocalIp(ref connection);
        BindCpuUtilization(ref connection);
        BindOccupiedMemory(ref connection);
        BindTotalMemory(ref connection);
    }

    private static void BindTotalMemory(ref HubConnection connection)
    {
        connection.On("GetTotalMemory", new GetTotalMemoryMethodExecutor().Execute);
    }

    private static void BindOccupiedMemory(ref HubConnection connection)
    {
        connection.On("GetOccupiedMemory", new GetOccupiedMemoryMethodExecutor().Execute);
    }

    private static void BindCpuUtilization(ref HubConnection connection)
    {
        connection.On("GetCpuUtilization", new GetCpuUtilizationMethodExecutor().Execute);
    }

    private static void BindGetLocalIp(ref HubConnection connection)
    {
        connection.On("GetLocalIp", new GetLocalIpMethodExecutor().Execute);
    }

    private static void BindGetCpuName(ref HubConnection connection)
    {
        connection.On("GetCpuName", new GetCpuName().Execute);
    }

    private static void BindGetOperatingSystem(ref HubConnection connection)
    {
        connection.On("GetOperatingSystem", new GetOperatingSystemMethodExecutor().Execute);
    }

    private static void BindShutdown(ref HubConnection connection)
    {
        connection.On("ShutDown", new ShutdownMethodExecutor().Execute);
    }

    private static void BindGetName(ref HubConnection connection)
    {
        connection.On("GetDeviceName", new GetDeviceNameMethodExecutor().Execute);
    }
}