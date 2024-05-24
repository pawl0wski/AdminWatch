using AdminWatchClient.ServerConnector.Requests;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector.Senders;

public class UpdateDeviceInfoSender(ConnectionState connectionState, IHardwareService hardwareService)
    : BaseServerConnectorSender<UpdateDeviceInfoRequest, bool>(connectionState)
{
    protected override async Task<bool> SendRequest(HubConnection connection, UpdateDeviceInfoRequest request)
    {
        return await connection.InvokeAsync<bool>("UpdateDeviceInfo", request);
    }

    protected override UpdateDeviceInfoRequest PrepareRequest()
        => new ()
        {
            DeviceId = GetDeviceId(),
            Ip = hardwareService.GetLocalIp(),
            Os = hardwareService.GetOperatingSystem(),
            ProcessorName = hardwareService.GetProcessorName(),
            TotalMemory = hardwareService.GetTotalMemory(),
            Manufacturer = hardwareService.GetManufacturer(),
            Battery = hardwareService.GetBatteryStatus()
        };
}