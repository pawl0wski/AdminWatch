using Hardware.Info;

namespace AdminWatchClient.Services.ServerConnector.Actions;

public class GetCpuName : IBaseAction<string>
{
    public string Execute()
    {
        var hi = new HardwareInfo();
        hi.RefreshCPUList();

        return hi.CpuList.First().Name;
    }
}

