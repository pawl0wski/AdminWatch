using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Obtainers;

public static class DeviceInfoObtainer
{
    public static async Task<DeviceInfo> FromClient(ISingleClientProxy clientProxy)
    {
        return new DeviceInfo
        {
            Os = await GetOperatingSystem(clientProxy),
            Ip = await GetLocalIp(clientProxy),
            ProcessorName = await GetCpuName(clientProxy),
            MacAdress = "TODO"
        };
    }
    
    private static Task<string> GetOperatingSystem(ISingleClientProxy clientProxy, CancellationToken? cancellationToken = null) =>
        clientProxy.InvokeAsync<string>("GetOperatingSystem", cancellationToken ?? new CancellationToken());
    
    private static Task<string> GetLocalIp(ISingleClientProxy clientProxy, CancellationToken? cancellationToken = null) =>
        clientProxy.InvokeAsync<string>("GetLocalIp", cancellationToken ?? new CancellationToken());
    
    private static Task<string> GetCpuName(ISingleClientProxy clientProxy, CancellationToken? cancellationToken = null) =>
        clientProxy.InvokeAsync<string>("GetCpuName", cancellationToken ?? new CancellationToken());
    
}