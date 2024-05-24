namespace AdminWatchClient.ServerConnector.Requests;

public class ConnectToServerRequest : HubRequest
{
    public required string Name { get; set; }

}