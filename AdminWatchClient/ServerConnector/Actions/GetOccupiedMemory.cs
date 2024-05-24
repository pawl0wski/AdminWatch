using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetOccupiedMemoryMethodExecutor : IBaseMethodExecutor<(int, int)>
{
    private readonly HardwareInfo _hardwareInfo = new();
    
    public (int, int) Execute()
    {
        _hardwareInfo.RefreshMemoryStatus();

        var totalMemory = _hardwareInfo.MemoryStatus.TotalPhysical;
        var occupiedMemory = totalMemory - _hardwareInfo.MemoryStatus.AvailablePhysical;

        return (Convert.ToInt32(totalMemory), Convert.ToInt32(occupiedMemory));
    }
}