using AdminWatchServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer.Services.Devices;

public class DevicesRepository(AdminWatchContext context) : IDevicesRepository
{
    public List<Device> GetAllDevices()
        => context.Devices
            .Include(dev => dev.Info)
            .Include(dev => dev.CpuUtilizations)
            .ToList();

    public List<Device> GetAllConnectedDevices()
        => GetAllDevices().Where(d => d.IsConnected()).ToList();
}