using AdminWatchServer.DevicesConnector.Requests;
using AdminWatchServer.Services.Devices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer.DevicesConnector.Actions;

public class UpdateDeviceInfoHubMethodExecutor(IDevicesRepository devicesRepository, DbContext context)
    : IHubMethodExecutor<bool, UpdateDeviceInfoRequest>
{
    public async Task<bool> Execute(ISingleClientProxy sender, HubCallerContext callerContext, UpdateDeviceInfoRequest req)
    {
        var device = devicesRepository.GetDevice(req.DeviceId);

        req.UpdateDeviceInfo(device.Info);
        await context.SaveChangesAsync();

        return true;
    }
}