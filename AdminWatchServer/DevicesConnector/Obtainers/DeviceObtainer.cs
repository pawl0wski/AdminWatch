using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Obtainers;

public static class DeviceObtainer
{
    public static async Task<Device> FromClient(ISingleClientProxy clientProxy)
    {
        var device = new Device
        {
            Id = Guid.NewGuid(),
            Name = await GetDeviceName(clientProxy),
            Info = await DeviceInfoObtainer.FromClient(clientProxy)
        };
        return device;
    }

    private static Task<string> GetDeviceName(ISingleClientProxy clientProxy, CancellationToken? cancellationToken = null) =>
        clientProxy.InvokeAsync<string>("GetDeviceName", cancellationToken ?? new CancellationToken());
}

    