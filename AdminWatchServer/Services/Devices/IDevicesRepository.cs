using AdminWatchServer.Models;

namespace AdminWatchServer.Services.Devices;

public interface IDevicesRepository
{
    public Device GetDevice(Guid deviceId);
    public List<Device> GetAllDevicesWithoutMeasures();

    public List<Device> GetAllConnectedDevices();

    public List<DeviceCpuUtilization> GetLastCpuUtilization(Guid deviceId);
    
    public List<DeviceMemoryOccupy> GetLastMemoryOccupies(Guid deviceId);

    public double GetTotalMemory(Guid deviceId);
}

