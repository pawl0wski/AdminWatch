namespace AdminWatchClient.ServerConnector.Requests;

public abstract class HubRequest
{
    public required Guid DeviceId { get; set; }

}