using AdminWatchClient.Services;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetOperatingSystemMethodExecutor(IHardwareService hardwareService)
    : IBaseMethodExecutor<string>
{
    public string Execute()
        => hardwareService.GetOperatingSystem();
}

