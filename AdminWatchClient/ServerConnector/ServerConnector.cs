using AdminWatchClient.ServerConnector.Exceptions;
using AdminWatchClient.ServerConnector.Requests;
using AdminWatchClient.ServerConnector.Senders;
using AdminWatchClient.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminWatchClient.ServerConnector;

public class ServerConnector : IServerConnector
{
    private ConnectionState? state;

    private ILogger<ServerConnector> _logger;
    
    private readonly IHardwareService _hardwareService;

    private HubConnection _connection;

    private ConnectToBaseServerSender? _connectToServerSender;

    private UpdateDeviceInfoSender? _updateDeviceInfoSender;

    private AddDeviceCpuUtilizationSender? _addDeviceCpuUtilizationSender;

    private AddDeviceMemoryOccupySender? _addDeviceMemoryOccupySender;

    public ServerConnector(
        IConfiguration config,
        IHardwareService hardwareService,
        ILogger<ServerConnector> logger)
    {
        _hardwareService = hardwareService;
        _logger = logger;
        state = ConnectionState.LoadOrDefault();

        CreateConnection(config);
        InitializeSenders();

        ServerConnectorMethodExecutorBinder.BindAllActions(ref _connection!);
    }

    private void InitializeSenders()
    {
        if (state is null)
            throw new NullReferenceException();

        _connectToServerSender = new ConnectToBaseServerSender(state);
        _updateDeviceInfoSender = new UpdateDeviceInfoSender(state, _hardwareService);
        _addDeviceCpuUtilizationSender = new AddDeviceCpuUtilizationSender(state, _hardwareService);
        _addDeviceMemoryOccupySender = new AddDeviceMemoryOccupySender(state, _hardwareService);
    }

    private void CreateConnection(IConfiguration config)
    {
        var serverIpAndPort = config.GetValue<string>("ip");
        _connection = new HubConnectionBuilder()
            .WithUrl($"http://{serverIpAndPort}/DevicesConnector")
            .Build();

        _connection.Closed += _ =>
        {
            _logger.LogInformation("Server closed connection.");
            Environment.Exit(0);
            return Task.CompletedTask;
        };
    }

    public async Task Connect()
    {
        try
        {
            await _connection.StartAsync();
        }
        catch (Exception)
        {
            throw new ConnectToServerFailException("Can't connect to AdminWatch server.");
        }

        if (state is null)
            throw new NullReferenceException();

        await RegisterInServer();
    }

    private async Task RegisterInServer()
    {
        if (state is null)
            throw new NullReferenceException();

        try
        {
            state.DeviceId = await _connectToServerSender!.Execute(_connection);
        }
        catch (Exception)
        {
            _logger.LogWarning("Server doesn't recognized saved DeviceId.");
            state.DeviceId = Guid.Empty;
            state.DeviceId = await _connectToServerSender!.Execute(_connection);
        }

        state.Save();
    }

    public Task UpdateDeviceInfo()
        => _updateDeviceInfoSender!.Execute(_connection);

    public Task AddDeviceCpuUtilization()
        => _addDeviceCpuUtilizationSender!.Execute(_connection);

    public Task AddDeviceMemoryOccupy()
        => _addDeviceMemoryOccupySender!.Execute(_connection);
}