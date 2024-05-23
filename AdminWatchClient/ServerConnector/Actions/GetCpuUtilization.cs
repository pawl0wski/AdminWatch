using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetCpuUtilization 
    : IBaseMethodExecutor<int>
{
    private HardwareInfo _hardwareInfo = new();
    
    public int Execute()
    {
        _hardwareInfo.RefreshCPUList();

        var cpu = _hardwareInfo.CpuList.First();
        var cpuUtilization = 
            cpu.CpuCoreList.Sum(
                core => Convert.ToInt32(core.PercentProcessorTime));
        cpuUtilization /= cpu.CpuCoreList.Count;
        
        return cpuUtilization;
    }
}