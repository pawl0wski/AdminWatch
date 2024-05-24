namespace AdminWatchClient.ServerConnector.Requests;

public class AddDeviceCpuUtilizationRequest : HubRequest
{
    public int Utilization { get; set; }

}