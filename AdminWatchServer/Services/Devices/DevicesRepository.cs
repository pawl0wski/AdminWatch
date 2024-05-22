using AdminWatchServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer.Services.Devices;

public class DevicesRepository(AdminWatchContext context) : IDevicesRepository
{
    public List<Device> GetAllDevices()
    {
        return context.Devices
            .Include(dev => dev.Info)
            .ToList();
    }
}