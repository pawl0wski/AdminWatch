using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Obtainers;

public static class DeviceCpuUtilizationObtainer
{
    public static async Task<DeviceCpuUtilization> GetFromClient(ISingleClientProxy clientProxy)
    {
        return new DeviceCpuUtilization
        {
            MeasureTime = DateTime.Now,
            Utilization = await GetCpuUtilization(clientProxy)
        };
    }

    private static async Task<int> GetCpuUtilization(ISingleClientProxy clientProxy, CancellationToken? cancellationToken = null)
        => await clientProxy.InvokeAsync<int>("GetCpuUtilization", cancellationToken ?? new CancellationToken());
}