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
            .First(dev => dev.Id == deviceId);

    public List<Device> GetAllDevicesWithoutMeasures()
        => context.Devices
            .Include(dev => dev.Info)
            .ToList();

    public List<Device> GetAllConnectedDevices()
        => GetAllDevicesWithoutMeasures().Where(d => d.IsConnected()).ToList();

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

    public double GetTotalMemory(Guid deviceId)
        => context.Devices
            .Include(d => d.Info)
            .First(d => d.Id == deviceId)
            .Info.TotalMemory;
}