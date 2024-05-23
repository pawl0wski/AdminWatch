using Hardware.Info;

namespace AdminWatchClient.Services.ServerConnector.Actions;

public class GetOperatingSystemAction : IBaseAction<string>
{
    private readonly HardwareInfo _hardwareInfo = new();
    public string Execute()
    {
        _hardwareInfo.RefreshOperatingSystem();

        return _hardwareInfo.OperatingSystem.Name;
    }
}

