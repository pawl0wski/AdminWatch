using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Obtainers;

public static class DeviceMemoryOccupyObtainer
{
    public static async Task<DeviceMemoryOccupy> GetFromClient(ISingleClientProxy client)
    {

        var memoryData = await GetOccupiedMemory(client);
        
        return new DeviceMemoryOccupy
        {
            OccupiedMemory = memoryData.First(),
            TotalMemory = memoryData.Last(),
            MeasureTime = DateTime.Now
        };
    }

    private static Task<double[]> GetOccupiedMemory(ISingleClientProxy client,
        CancellationToken? cancellationToken = null)
        => client.InvokeAsync<double[]>("GetOccupiedMemory", cancellationToken ?? new CancellationToken());
}