using Hardware.Info;

namespace AdminWatchClient.Services.ServerConnector.Actions;

public class GetOperatingSystemAction : IBaseAction<string>
{
    public string Execute()
    {
        var hi = new HardwareInfo();
        hi.RefreshOperatingSystem();

        return hi.OperatingSystem.Name;
    }
}

