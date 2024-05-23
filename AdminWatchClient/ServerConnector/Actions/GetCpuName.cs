using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetCpuName : IBaseMethodExecutor<string>
{
    private readonly HardwareInfo _hardwareInfo = new();

    public string Execute()
    {
        _hardwareInfo.RefreshCPUList();

        return _hardwareInfo.CpuList.First().Name;
    }
}

