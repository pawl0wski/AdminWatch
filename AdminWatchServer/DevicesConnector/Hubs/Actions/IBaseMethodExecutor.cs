using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Hubs.Actions;

public interface IBaseMethodExecutor<TResult, in TIn>
{
    public Task<TResult> Execute(ISingleClientProxy sender, TIn input);
}