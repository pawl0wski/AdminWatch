using System.Diagnostics;
using AdminWatchClient.ServerConnector.Models;
using AdminWatchClient.ServerConnector.Responses;

namespace AdminWatchClient.ServerConnector.Actions;

public class GetProcessesMethodExecutor 
    : BaseMethodExecutor<GetProcessesRespone, object>
{
    public override GetProcessesRespone Execute(object req)
    {
        Console.WriteLine("GET PROCESSES!!!!!");
        var response = new GetProcessesRespone();
        var processes = Process.GetProcesses();
     
        
        foreach (var process in processes)
        {
            var respProcess = DeviceProcess.FromSystemProcess(process);
            response.Processes.Add(respProcess);
        }

        return response;
    }
}