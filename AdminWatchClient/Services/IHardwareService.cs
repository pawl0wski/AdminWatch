namespace AdminWatchClient.Services;

public interface IHardwareService
{
    public double GetOccupiedMemory();

    public int GetCpuUtilization();

    public string GetOperatingSystem();

    public double GetTotalMemory();

    public string GetProcessorName();

    public string GetLocalIp();
}