namespace AdminWatchClient.Services;

public interface IServerService
{
    public Action? OnShutdown { get; set; }

    public Task Connect();
}