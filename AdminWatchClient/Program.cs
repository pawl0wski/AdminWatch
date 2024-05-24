using AdminWatchClient.ServerConnector;
using AdminWatchClient.ServerConnector.Exceptions;
using AdminWatchClient.Services;

namespace AdminWatchClient;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length < 2)
            PrintHelp();
        
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<IHardwareService, HardwareService>();
        
        builder.Services.AddHostedService<Worker>();
        
        builder.Services.AddSingleton<IServerConnector, ServerConnector.ServerConnector>();
        
        var host = builder.Build();

        await host.RunAsync();
    }

    private static void PrintHelp()
    {
        Console.WriteLine("AdminWatch Client");
        Console.WriteLine("--ip - Server's IP and port");
        Console.WriteLine($"ex. {Environment.CommandLine} --ip 127.0.0.1:8000");
        Environment.Exit(0);
    }

}