using AdminWatchClient.ServerConnector.Requests;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector.Senders;

public class AddDeviceCpuUtilizationSender(ConnectionState connectionState, IHardwareService service)
    : BaseServerConnectorSender<AddDeviceCpuUtilizationRequest, bool>(connectionState)
{
    protected override async Task<bool> SendRequest(HubConnection connection, AddDeviceCpuUtilizationRequest request)
    {
        return await connection.InvokeAsync<bool>("AddDeviceCpuUtilization", request);
    }

    protected override AddDeviceCpuUtilizationRequest PrepareRequest()
        => new()
        {
            DeviceId = GetDeviceId(),
            Utilization = service.GetCpuUtilization()
        };
}
