namespace AdminWatchClient.ServerConnector.Actions;

public interface IBaseMethodExecutor<out TResult>
{
    public TResult Execute();
}