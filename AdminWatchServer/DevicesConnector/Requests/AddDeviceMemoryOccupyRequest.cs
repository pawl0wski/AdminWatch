using AdminWatchServer.Models;

namespace AdminWatchServer.DevicesConnector.Requests;

public class AddDeviceMemoryOccupyRequest : BaseDevicesConnectorRequest
{
    public double OccupiedMemory { get; set; }

    public DeviceMemoryOccupy CreateDeviceMemoryOccupy()
        => new()
        {
            OccupiedMemory = OccupiedMemory,
            MeasureTime = DateTime.Now
        };
}