using AdminWatchServer.Models;

namespace AdminWatchServer.Services.Devices;

public interface IDevicesRepository
{
    public Device GetDevice(Guid deviceId);
    public List<Device> GetAllDevicesWithoutMeasures();

}

