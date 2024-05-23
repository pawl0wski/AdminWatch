using AdminWatchServer.DevicesConnector.Obtainers;
using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Hubs.Actions;

public class ConnectToServerMethodExecutor(AdminWatchContext context) : IBaseMethodExecutor<string, string>
{
    public async Task<string> Execute(ISingleClientProxy sender, string stringDeviceId)
    {
        Device device;
        if (stringDeviceId == "")
        {
            device = await CreateNewDevice(sender);
        }
        else
        {
            var deviceId = Guid.Parse(stringDeviceId);
            device = context.Devices.First(d => d.Id == deviceId);
        }
        device.Status = Device.DeviceStatus.Connected;

        return device.Id.ToString();
    }

    private async Task<Device> CreateNewDevice(ISingleClientProxy sender)
    {
        var newDevice = await DeviceObtainer.FromClient(sender);
        context.Devices.Add(newDevice);
        await context.SaveChangesAsync();
        return newDevice;
    }
}

