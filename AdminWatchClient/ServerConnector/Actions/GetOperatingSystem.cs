using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetOperatingSystemMethodExecutor : IBaseMethodExecutor<string>
{
    private readonly HardwareInfo _hardwareInfo = new();
    public string Execute()
    {
        _hardwareInfo.RefreshOperatingSystem();

        return _hardwareInfo.OperatingSystem.Name;
    }
}

