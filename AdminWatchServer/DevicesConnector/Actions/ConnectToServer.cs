using AdminWatchServer.DevicesConnector.Requests;
using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Actions;

public class ConnectToServerHubMethodExecutor(AdminWatchContext context)
    : IHubMethodExecutor<string, ConnectToServerRequest>
{
    public async Task<string> Execute(
        ISingleClientProxy sender,
        HubCallerContext callerContext,
        ConnectToServerRequest req)
    {
        Device device;

        if (req.DeviceId == Guid.Empty)
            device = CreateNewDevice(req);
        else
            device = GetExistingDevice(req.DeviceId);

        SetDeviceStatusToConnected(device, callerContext.ConnectionId);

        await context.SaveChangesAsync();

        return device.Id.ToString();
    }

    private void SetDeviceStatusToConnected(Device device, string connectionId)
    {
        device.Status = Device.DeviceStatus.Connected;
        device.ConnectionId = connectionId;
    }

    private Device GetExistingDevice(Guid deviceId)
    {
        return context.Devices.First(d => d.Id == deviceId);
    }

    private Device CreateNewDevice(ConnectToServerRequest req)
    {
        var newDevice = req.CreateDevice();
        context.Devices.Add(newDevice);
        return newDevice;
    }
}