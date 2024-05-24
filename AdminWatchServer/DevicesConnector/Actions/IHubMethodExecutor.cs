using AdminWatchServer.DevicesConnector.Requests;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Actions;

public interface IHubMethodExecutor<TResult, in TIn>
    where TIn : BaseDevicesConnectorRequest
{
    public Task<TResult> Execute(ISingleClientProxy sender, HubCallerContext callerContext, TIn req);
}