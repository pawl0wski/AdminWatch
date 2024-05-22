using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Hubs;

public class DevicesConnectorHub(AdminWatchContext context) : Hub
{
    public Task Test()
    {
        Console.WriteLine("Test");
        return Task.CompletedTask;
    }

    public async Task ConnectToServer(string stringDeviceId)
    {
        Guid deviceId;
        if (stringDeviceId == "")
        {
            deviceId = Guid.NewGuid();
            var deviceName = await Clients.Caller.InvokeAsync<string>("GetDeviceName", new CancellationToken());
            var os = await Clients.Caller.InvokeAsync<string>("GetOperatingSystem", new CancellationToken());
            var processorName = await Clients.Caller.InvokeAsync<string>("GetCpuName", new CancellationToken());
            var newDevice = new Device
            {
                Id = deviceId,
                Name = deviceName,
                Info = new DeviceInfo
                {
                    Ip = "localhost",
                    MacAdress = "localhost",
                    Os = os,
                    ProcessorName = processorName
                }
                
            };
            context.Devices.Add(newDevice);
        }
        else
        {
            deviceId = Guid.Parse(stringDeviceId);
            var device = context.Devices.First(device => device.Id == deviceId);
            device.Status = Device.DeviceStatus.Connected;
        }

        await context.SaveChangesAsync();
    }
}