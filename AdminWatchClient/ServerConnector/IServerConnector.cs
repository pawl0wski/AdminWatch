namespace AdminWatchClient.ServerConnector;

public interface IServerConnector
{
    public Task Connect();

    public Task UpdateDeviceInfo();

    public Task AddDeviceCpuUtilization();

    public Task AddDeviceMemoryOccupy();
}