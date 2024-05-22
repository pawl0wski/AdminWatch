using AdminWatchClient.Services;

namespace AdminWatchClient;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddSingleton<IServerService, ServerService>();
        
        var host = builder.Build();

        await ConfigureServerService(host);
  
        await host.RunAsync();
    }

    private static async Task ConfigureServerService(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IServerService>();
        service.OnShutdown = () =>
        {
            Console.WriteLine("Shutdown!");
        };

        await ConnectServerService(service);
        await service.Connect();
    }

    private static async Task ConnectServerService(IServerService service)
    {
        try
        {
            await service.Connect();
        }
        catch (HttpRequestException)
        {
            await Console.Error.WriteLineAsync("Problem podczas łączenia z serwerem. Sprawdź IP.");
        }
    }
}