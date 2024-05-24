using AdminWatchServer.Models;

namespace AdminWatchServer.Services.Devices;

public interface IDevicesRepository
{
    public List<Device> GetAllDevices();

    public List<Device> GetAllConnectedDevices();

    public List<DeviceCpuUtilization> GetLastCpuUtilization(Guid deviceId);
    public List<DeviceMemoryOccupy> GetLastMemoryOccupies(Guid deviceId);
}

