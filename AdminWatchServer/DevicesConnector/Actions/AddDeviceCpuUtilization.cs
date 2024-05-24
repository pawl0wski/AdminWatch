using AdminWatchServer.DevicesConnector.Requests;
using AdminWatchServer.Services.Devices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer.DevicesConnector.Actions;

public class AddDeviceCpuUtilization(IDevicesRepository devicesRepository, DbContext context) 
    : IHubMethodExecutor<bool, AddDeviceCpuUtilizationRequest>
{
    public async Task<bool> Execute(ISingleClientProxy sender, HubCallerContext callerContext, AddDeviceCpuUtilizationRequest req)
    {
        var device = devicesRepository.GetDevice(req.DeviceId);
        
        device.CpuUtilizations.Add(req.CreateDeviceCpuUtilization());
        await context.SaveChangesAsync();

        return true;
    }
    
}