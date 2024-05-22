using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdminWatchClient.Services.ServerConnector.Actions;

public class ShutdownAction : IBaseAction<object>
{
    public object Execute()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start("shutdown", "now");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Process.Start("shutdown","/s /t 0");
        
        return null;
    }
}