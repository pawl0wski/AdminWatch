using AdminWatchServer.Models;

namespace AdminWatchServerTests;

public class DeviceModelTests
{

    [Test]
    public void TestIsConnectedIfConnectionIdIsNull()
    {
        var dev = new Device
        {
            Id = Guid.Empty,
            Name = "TestDev",
            Status = Device.DeviceStatus.Connected,
            ConnectionId = null,
            Info = new DeviceInfo(),
            CpuUtilizations = [],
            MemoryOccupies = []
        };

        var result = dev.IsConnected();
        
        Assert.That(result, Is.False);
    }    
    
    [Test]
    public void TestIsConnectedIfStatusIsDisconnected()
    {
        var dev = new Device
        {
            Id = Guid.Empty,
            Name = "TestDev",
            Status = Device.DeviceStatus.Disconnected,
            ConnectionId = "conn",
            Info = new DeviceInfo(),
            CpuUtilizations = [],
            MemoryOccupies = []
        };

        var result = dev.IsConnected();
        
        Assert.That(result, Is.False);
    }    
    
    [Test]
    public void TestIsConnectedIfEverythingOk()
    {
        var dev = new Device
        {
            Id = Guid.Empty,
            Name = "TestDev",
            Status = Device.DeviceStatus.Connected,
            ConnectionId = "conn",
            Info = new DeviceInfo(),
            CpuUtilizations = [],
            MemoryOccupies = []
        };

        var result = dev.IsConnected();
        
        Assert.That(result, Is.True);
    }
}