namespace AdminWatchClient.ServerConnector.Actions;

public abstract class BaseMethodExecutor<TResponse, TRequest>
{
    public abstract TResponse Execute(TRequest request);
}