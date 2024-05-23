using AdminWatchClient.ServerConnector;
using AdminWatchClient.ServerConnector.Exceptions;

namespace AdminWatchClient;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length < 2)
            PrintHelp();
        
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();

        
        builder.Services.AddSingleton<IServerConnectorService, ServerConnectorService>();
        
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
        var service = scope.ServiceProvider.GetRequiredService<IServerConnectorService>();

        await ConnectServerService(service);
    }

    private static async Task ConnectServerService(IServerConnectorService connectorService)
    {
        try
        {
            await connectorService.Connect();
        }
        catch (ConnectToServerFailException)
        {
            await Console.Error.WriteLineAsync("Can't connect to AdminWatch Server.\nCheck provided IP Address.");
            Environment.Exit(1);
        }
    }
}