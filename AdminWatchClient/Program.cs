using AdminWatchClient.Services.ServerConnector;
using AdminWatchClient.Services.ServerConnector.Exceptions;

namespace AdminWatchClient;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddSingleton<IServerConnectorService, ServerConnectorService>();
        
        var host = builder.Build();

        await ConfigureServerService(host);
  
        await host.RunAsync();
    }

    private static async Task ConfigureServerService(IHost host)
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