using System.Net.NetworkInformation;
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

    public string GetProcessorName()
    {
        _hardwareInfo.RefreshCPUList();
        
        return _hardwareInfo.CpuList.First().Name;
    }

    public string GetLocalIp()
    {
        var addresses =
            HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Ethernet, OperationalStatus.Up);
        return addresses.First().ToString();
    }

    public string GetManufacturer()
    {
        _hardwareInfo.RefreshBIOSList();
        var bios = _hardwareInfo.BiosList.First();

        return bios.Manufacturer;
    }

    public int GetBatteryStatus()
    {
        _hardwareInfo.RefreshBatteryList();
        if (_hardwareInfo.BatteryList.Count == 0)
            return 0;
        
        var battery = _hardwareInfo.BatteryList.First();

        return battery.EstimatedChargeRemaining;
    }

    private double ConvertToGb(ulong bytes)
        => Convert.ToDouble(bytes) / 1024 / 1024 / 1024;
}