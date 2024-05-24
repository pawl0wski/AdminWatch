using AdminWatchClient.Services;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetCpuName(IHardwareService hardwareService) : IBaseMethodExecutor<string>
{
    public string Execute() 
        => hardwareService.GetCpuName();
}

