using AdminWatchServer.DevicesConnector.Models;

namespace AdminWatchServer.DevicesConnector.Responses;

public class GetProcessesRespone : HubResponse
{

    public List<DeviceProcess> Processes { get; set; } = [];

    
}