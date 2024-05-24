namespace AdminWatchClient.ServerConnector.Actions;

public abstract class BaseMethodExecutor<TResult>
{
    public abstract TResult Execute();
}