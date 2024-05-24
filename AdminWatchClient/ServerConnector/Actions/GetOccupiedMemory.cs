using AdminWatchClient.Services;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetOccupiedMemoryMethodExecutor(IHardwareService hardwareService)
    : IBaseMethodExecutor<double>
{
    public double Execute()
        => hardwareService.GetOccupiedMemory();

}