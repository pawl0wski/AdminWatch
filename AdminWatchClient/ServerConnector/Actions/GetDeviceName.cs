namespace AdminWatchClient.ServerConnector.Actions;

public class GetDeviceNameMethodExecutor : IBaseMethodExecutor<string> {
    public string Execute()
    {
        return Environment.MachineName;
    }
}