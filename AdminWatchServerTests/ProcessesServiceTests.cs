using AdminWatchServer.DevicesConnector;
using AdminWatchServer.Models;
using AdminWatchServer.Services.Processes;
using Microsoft.AspNetCore.SignalR;

namespace AdminWatchServerTests;

public class AuthServiceTest
{

    private Mock<IHubContext<DevicesConnectorHub>> _mockedHubContext;

    private Mock<Device> _mockedDevice;
    
    [SetUp]
    public void Setup()
    {
        _mockedHubContext = new Mock<IHubContext<DevicesConnectorHub>>();
        _mockedDevice = new Mock<Device>();
    }

    [Test]
    public void TestGettingProcessessIfDeviceIsDisconnected()
    {
        _mockedDevice.Setup(dev => dev.IsConnected()).Returns(false);
        var processesService = new ProcessesService(_mockedHubContext.Object);
        
        Assert.ThrowsAsync<Exception>( async ()  => await processesService.GetProcessesForDevice(_mockedDevice.Object));
    }

}