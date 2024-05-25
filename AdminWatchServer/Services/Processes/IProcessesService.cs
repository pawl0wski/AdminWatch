using AdminWatchServer.DevicesConnector.Models;
using AdminWatchServer.Models;

namespace AdminWatchServer.Services.Processes;

public interface IProcessessService
{
    public Task<List<DeviceProcess>> GetProcessesForDevice(Device device, CancellationToken? cancellationToken = null);

    public Task<bool> KillProcess(DeviceProcess process, Device device, CancellationToken? cancellationToken = null);
}
