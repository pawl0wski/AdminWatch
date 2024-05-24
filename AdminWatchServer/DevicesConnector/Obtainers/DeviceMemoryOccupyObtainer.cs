using AdminWatchServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServer.DevicesConnector.Obtainers;

public static class DeviceMemoryOccupyObtainer
{
    public static async Task<DeviceMemoryOccupy> GetFromClient(ISingleClientProxy client)
    {

        var occupiedMemory = await GetOccupiedMemory(client);
        
        return new DeviceMemoryOccupy
        {
            OccupiedMemory = occupiedMemory,
            MeasureTime = DateTime.Now
        };
    }

    private static Task<double> GetOccupiedMemory(ISingleClientProxy client,
        CancellationToken? cancellationToken = null)
        => client.InvokeAsync<double>("GetOccupiedMemory", cancellationToken ?? new CancellationToken());
}