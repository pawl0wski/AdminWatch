using AdminWatchServer.Models;

namespace AdminWatchServer.DevicesConnector.Requests;

public class ConnectToServerRequest : BaseDevicesConnectorRequest
{
    public required string Name { get; set; }

    public Device CreateDevice()
        => new()
        {
            Name = Name,
            CpuUtilizations = [],
            MemoryOccupies = [],
            Info = new DeviceInfo()
        };
}