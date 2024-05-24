using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetTotalMemoryMethodExecutor : IBaseMethodExecutor<double>
{
    private readonly HardwareInfo _hardwareInfo = new();
    
    public double Execute()
    {
        _hardwareInfo.RefreshMemoryStatus();

        var totalMemory = _hardwareInfo.MemoryStatus.TotalPhysical;

        return Math.Round(ConvertToGb(totalMemory), 2);
    }
    
    private double ConvertToGb(ulong bytes)
        => Convert.ToDouble(bytes) / 1024 / 1024 / 1024;
}