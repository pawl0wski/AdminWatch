using AdminWatchClient.ServerConnector.Models;

namespace AdminWatchClient.ServerConnector.Responses;

public class GetProcessesRespone : HubResponse
{
    public List<DeviceProcess> Processes { get; set; } = [];

}