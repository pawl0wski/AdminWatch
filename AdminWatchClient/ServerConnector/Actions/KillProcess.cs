using System.Diagnostics;
using AdminWatchClient.ServerConnector.Models;

namespace AdminWatchClient.ServerConnector.Actions;

public class KillProcessMethodExecutor 
    : BaseMethodExecutor<bool, DeviceProcess>
{
    public override bool Execute(DeviceProcess req)
    {
        Process process;
        try
        {
            process = Process.GetProcessById(req.Id);
        }
        catch (ArgumentException)
        {
            return false;
        }

        process.Kill();
        return true;
    }
}