using AdminWatchServer.Models;

namespace AdminWatchServer.Services.Devices;

public interface IDevicesRepository
{
    public List<Device> GetAllDevices();
}

