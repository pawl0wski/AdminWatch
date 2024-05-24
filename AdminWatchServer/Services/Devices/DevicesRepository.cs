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

    public List<DeviceCpuUtilization> GetLastCpuUtilization(Guid deviceId)
        => context.Devices
            .Include(d => d.CpuUtilizations)
            .First(d => d.Id == deviceId)
            .CpuUtilizations.ToList();

    public List<DeviceMemoryOccupy> GetLastMemoryOccupies(Guid deviceId)
        => context.Devices
            .Include(d => d.MemoryOccupies)
            .First(d => d.Id == deviceId)
            .MemoryOccupies.ToList();
}