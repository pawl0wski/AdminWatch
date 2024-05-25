using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdminWatchClient.ServerConnector.Actions;

public class ShutdownMethodExecutor : BaseMethodExecutor<bool, object>
{
    public override bool Execute(object req)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start("shutdown", "now");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Process.Start("shutdown","/s /t 0");
        
        return true;
    }
}