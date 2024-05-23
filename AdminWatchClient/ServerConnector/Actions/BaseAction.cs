namespace AdminWatchClient.ServerConnector.Actions;

public interface IBaseAction<out TResult>
{
    public TResult Execute();
}