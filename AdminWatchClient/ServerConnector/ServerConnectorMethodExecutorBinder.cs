using AdminWatchClient.ServerConnector.Actions;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public static class ServerConnectorMethodExecutorBinder
{
    
    public static void BindAllActions(ref HubConnection connection, IHardwareService service)
    {
        BindShutdown(ref connection);
        BindGetName(ref connection);
        BindGetOperatingSystem(ref connection, service);
        BindGetCpuName(ref connection, service);
        BindGetLocalIp(ref connection);
        BindCpuUtilization(ref connection, service);
        BindOccupiedMemory(ref connection, service);
        BindTotalMemory(ref connection, service);
    }

    private static void BindTotalMemory(ref HubConnection connection, IHardwareService service)
    {
        connection.On("GetTotalMemory", new GetTotalMemoryMethodExecutor(service).Execute);
    }

    private static void BindOccupiedMemory(ref HubConnection connection, IHardwareService service)
    {
        connection.On("GetOccupiedMemory", new GetOccupiedMemoryMethodExecutor(service).Execute);
    }

    private static void BindCpuUtilization(ref HubConnection connection, IHardwareService service)
    {
        connection.On("GetCpuUtilization", new GetCpuUtilizationMethodExecutor(service).Execute);
    }

    private static void BindGetLocalIp(ref HubConnection connection)
    {
        connection.On("GetLocalIp", new GetLocalIpMethodExecutor().Execute);
    }

    private static void BindGetCpuName(ref HubConnection connection, IHardwareService service)
    {
        connection.On("GetCpuName", new GetCpuName(service).Execute);
    }

    private static void BindGetOperatingSystem(ref HubConnection connection, IHardwareService service)
    {
        connection.On("GetOperatingSystem", new GetOperatingSystemMethodExecutor(service).Execute);
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