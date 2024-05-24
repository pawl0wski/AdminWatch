using AdminWatchClient.Services;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetTotalMemoryMethodExecutor(IHardwareService hardwareService)
    : IBaseMethodExecutor<double>
{
    public double Execute()
        => hardwareService.GetTotalMemory();
}