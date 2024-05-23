using System.Net.NetworkInformation;
using Hardware.Info;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetLocalIpMethodExecutor : IBaseMethodExecutor<string>
{
    public string Execute()
    {
        var iPv4Addresses =
            HardwareInfo.GetLocalIPv4Addresses(OperationalStatus.Up);

        return iPv4Addresses.First().ToString();
    }
}