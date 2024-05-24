namespace AdminWatchClient.ServerConnector.Requests;

public class AddDeviceMemoryOccupyRequest : HubRequest
{
    public double OccupiedMemory { get; set; }

}