using AdminWatchServer.Models;

namespace AdminWatchServer.DevicesConnector.Requests;

public class AddDeviceCpuUtilizationRequest : BaseDevicesConnectorRequest
{
    public int Utilization { get; set; }

    public DeviceCpuUtilization CreateDeviceCpuUtilization()
        => new()
        {
            Utilization = Utilization,
            MeasureTime = DateTime.Now
        };
}