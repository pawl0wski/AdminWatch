using AdminWatchServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer.Services.Devices;

public class DevicesRepository(AdminWatchContext context) : IDevicesRepository
{
    public Device GetDevice(Guid deviceId)
        => context.Devices
            .Include(dev => dev.Info)
            .Include(dev => dev.CpuUtilizations)
            .Include(dev => dev.MemoryOccupies)
            .AsSplitQuery()
            .First(dev => dev.Id == deviceId);

    public List<Device> GetAllDevicesWithoutMeasures()
        => context.Devices
            .Include(dev => dev.Info)
            .ToList();

    public async Task DisconnectAllDevices()
    {
        var devices = GetAllDevicesWithoutMeasures();
        foreach (var device in devices)
            device.Disconnect();

        await context.SaveChangesAsync();
    }
}