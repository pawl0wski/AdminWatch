using Hardware.Info;

namespace AdminWatchClient.Services;

public class HardwareService : IHardwareService
{
    private readonly HardwareInfo _hardwareInfo = new();

    public double GetOccupiedMemory()
    {
        _hardwareInfo.RefreshMemoryStatus();
        
        var totalMemory = _hardwareInfo.MemoryStatus.TotalPhysical;
        var occupiedMemory = totalMemory - _hardwareInfo.MemoryStatus.AvailablePhysical;

        return Math.Round(ConvertToGb(occupiedMemory), 2);    }

    public int GetCpuUtilization()
    {
        _hardwareInfo.RefreshCPUList();

        var cpu = _hardwareInfo.CpuList.First();
        var cpuUtilization =
            cpu.CpuCoreList.Sum(
                core => Convert.ToInt32(core.PercentProcessorTime));
        cpuUtilization /= cpu.CpuCoreList.Count;

        return cpuUtilization;
    }

    public string GetOperatingSystem()
    {
        _hardwareInfo.RefreshOperatingSystem();

        return _hardwareInfo.OperatingSystem.Name;
    }

    public double GetTotalMemory()
    {
        _hardwareInfo.RefreshMemoryStatus();

        var totalMemory = _hardwareInfo.MemoryStatus.TotalPhysical;

        return Math.Round(ConvertToGb(totalMemory), 2);    }

    public string GetCpuName()
    {
        _hardwareInfo.RefreshCPUList();
        
        return _hardwareInfo.CpuList.First().Name;
    }
    
    private double ConvertToGb(ulong bytes)
        => Convert.ToDouble(bytes) / 1024 / 1024 / 1024;
}