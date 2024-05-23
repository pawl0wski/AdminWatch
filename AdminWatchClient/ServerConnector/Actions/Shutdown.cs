using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdminWatchClient.ServerConnector.Actions;

public class ShutdownMethodExecutor : IBaseMethodExecutor<int>
{
    public int Execute()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start("shutdown", "now");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Process.Start("shutdown","/s /t 0");
        
        return 0;
    }
}