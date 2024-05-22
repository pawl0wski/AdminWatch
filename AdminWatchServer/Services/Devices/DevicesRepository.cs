using AdminWatchServer.Models;

namespace AdminWatchServer.Services.Devices;

public class DevicesRepository(AdminWatchContext context) : IDevicesRepository
{
    public List<Device> GetAllDevices()
    {
        return context.Devices.ToList();
    }
}