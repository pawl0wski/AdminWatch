using AdminWatchClient.Services;
using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetCpuUtilizationMethodExecutor(IHardwareService hardwareService)
    : IBaseMethodExecutor<int>
{
    public int Execute()
        => hardwareService.GetCpuUtilization();
}