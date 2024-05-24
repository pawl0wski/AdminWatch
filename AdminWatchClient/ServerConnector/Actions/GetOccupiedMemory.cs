using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetOccupiedMemoryMethodExecutor 
    : IBaseMethodExecutor<double[]>
{
    private readonly HardwareInfo _hardwareInfo = new();
    
    public double[] Execute()
    {
        _hardwareInfo.RefreshMemoryStatus();
        
        var totalMemory = _hardwareInfo.MemoryStatus.TotalPhysical;
        var occupiedMemory = totalMemory - _hardwareInfo.MemoryStatus.AvailablePhysical;

        return
        [
            ConvertToGb(totalMemory),
            ConvertToGb(occupiedMemory)
        ];
    }

    private double ConvertToGb(ulong bytes)
        => Convert.ToDouble(bytes) / 1024 / 1024 / 1024;
}