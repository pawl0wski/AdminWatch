using AdminWatchServer.Models;

namespace AdminWatchServer.DevicesConnector.Requests;

public class UpdateDeviceInfoRequest : BaseDevicesConnectorRequest
{
    public required string Os { get; set; }
    
    public required string Ip { get; set; }

    public required string ProcessorName { get; set; }

    public required double TotalMemory { get; set; }

    public void UpdateDeviceInfo( DeviceInfo deviceInfo)
    {
        deviceInfo.Os = Os;
        deviceInfo.Ip = Ip;
        deviceInfo.ProcessorName = ProcessorName;
        deviceInfo.TotalMemory = TotalMemory;
    }
} 