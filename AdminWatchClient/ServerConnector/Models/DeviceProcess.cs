using System.Diagnostics;

namespace AdminWatchClient.ServerConnector.Models;

public class DeviceProcess
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public static DeviceProcess FromSystemProcess(Process process)
    {
        var processName = process.ProcessName.Split(' ').First();
        return new DeviceProcess
        {
            Id = process.Id,
            Name = processName
        };
    }
}