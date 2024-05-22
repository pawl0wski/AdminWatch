namespace AdminWatchClient.Services.ServerConnector.Actions;

public interface IBaseAction<out TResult>
{
    public TResult Execute();
}