using AdminWatchServer.DevicesConnector.Requests;
using AdminWatchServer.Services.Devices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer.DevicesConnector.Actions;

public class AddDeviceMemoryOccupyMethodExecutor(IDevicesRepository devicesRepository, DbContext context) 
    : IHubMethodExecutor<bool, AddDeviceMemoryOccupyRequest>
{
    public async Task<bool> Execute(ISingleClientProxy sender, HubCallerContext callerContext, AddDeviceMemoryOccupyRequest req)
    {
        var device = devicesRepository.GetDevice(req.DeviceId);
        
        device.MemoryOccupies.Add(req.CreateDeviceMemoryOccupy());
        await context.SaveChangesAsync();

        return true;
    }
}
