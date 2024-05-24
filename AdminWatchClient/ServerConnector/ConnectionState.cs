
using System.Text.Json;

namespace AdminWatchClient.ServerConnector;
public class ConnectionState
{
    public Guid DeviceId { get; set; } = Guid.Empty;

    public static ConnectionState LoadOrDefault()
    {
        try
        {
            var serializedConfig = File.ReadAllText(GetConfigFilePath());
            return JsonSerializer.Deserialize<ConnectionState>(serializedConfig)!;
        }
        catch (Exception)
        {
            return new ConnectionState();
        }
    }

    private static  string GetConfigFilePath()
    {
        var configPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appConfigPath = Path.Join(configPath, "/AdminWatch/ClientState.json");
        Directory.CreateDirectory(appConfigPath);
        return appConfigPath;
    }
    public void Save()
    {
        File.WriteAllText(GetConfigFilePath(), JsonSerializer.Serialize(this));
    }
}