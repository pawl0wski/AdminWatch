namespace AdminWatchClient.ServerConnector.Actions;

public class GetDeviceNameAction : IBaseAction<string> {
    public string Execute()
    {
        return Environment.MachineName;
    }
}