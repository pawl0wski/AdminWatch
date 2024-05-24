namespace AdminWatchClient.ServerConnector.Requests;

public class UpdateDeviceInfoRequest : HubRequest
{
    public required string Os { get; set; }
    
    public required string Ip { get; set; }

    public required string ProcessorName { get; set; }

    public required double TotalMemory { get; set; }

    public required string Manufacturer { get; set; }

    public required int Battery { get; set; }
} 