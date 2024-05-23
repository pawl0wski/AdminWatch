using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.FluentUI.AspNetCore.Components;

namespace AdminWatchServer.DevicesConnector.Obtainers;

public static class DeviceObtainer
{
    public static async Task<Device> GetFromClient(ISingleClientProxy clientProxy)
    {
        var device = new Device
        {
            Id = Guid.NewGuid(),
            Name = await GetDeviceName(clientProxy),
            Info = await DeviceInfoObtainer.GetFromClient(clientProxy),
            CpuUtilizations = []
        };
        return device;
    }

    public static async Task UpdateFromClient(ISingleClientProxy clientProxy, Device device)
    {
        device.Name = await GetDeviceName(clientProxy);
        await DeviceInfoObtainer.UpdateFromClient(clientProxy, device.Info);
    }

    private static Task<string> GetDeviceName(ISingleClientProxy clientProxy, CancellationToken? cancellationToken = null) =>
        clientProxy.InvokeAsync<string>("GetDeviceName", cancellationToken ?? new CancellationToken());
}

    