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

        await ConfigureServerConnector(host);
  
        await host.RunAsync();
    }

    private static void PrintHelp()
    {
        Console.WriteLine("AdminWatch Client");
        Console.WriteLine("--ip - Server's IP and port");
        Console.WriteLine($"ex. {Environment.CommandLine} --ip 127.0.0.1:8000");
        Environment.Exit(0);
    }

    private static async Task ConfigureServerConnector(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IServerConnector>();

        await ConnectServerService(service);
    }

    private static async Task ConnectServerService(IServerConnector connector)
    {
        try
        {
            await connector.Connect();
        }
        catch (ConnectToServerFailException)
        {
            await Console.Error.WriteLineAsync("Can't connect to AdminWatch Server.\nCheck provided IP Address.");
            Environment.Exit(1);
        }
    }
}