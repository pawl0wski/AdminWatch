namespace AdminWatchClient.Services.ServerConnector.Actions;

public class ShutdownAction : IBaseAction<object>
{
    public object Execute()
    {
        Console.WriteLine("Shutdown.");
        return null;
    }
}