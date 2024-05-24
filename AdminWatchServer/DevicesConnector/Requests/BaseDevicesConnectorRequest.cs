namespace AdminWatchServer.DevicesConnector.Requests;

public abstract class BaseDevicesConnectorRequest
{
    public required Guid DeviceId { get; set; }

}