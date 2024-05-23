using System.Net.NetworkInformation;
using Hardware.Info;

namespace AdminWatchClient.Services.ServerConnector.Actions;

public class GetLocalIpAction : IBaseAction<string>
{
    public string Execute()
    {
        var iPv4Addresses =
            HardwareInfo.GetLocalIPv4Addresses(OperationalStatus.Up);

        return iPv4Addresses.First().ToString();
    }
}