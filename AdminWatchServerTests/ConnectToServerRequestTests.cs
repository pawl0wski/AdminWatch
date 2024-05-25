using AdminWatchServer.DevicesConnector.Requests;

namespace AdminWatchServerTests;

public class ConnectToServerRequestTests
{

    [Test]
    public void TestCreateDevice()
    {
        var req = new ConnectToServerRequest
        {
            DeviceId = Guid.Empty,
            Name = "TestName"
        };

        var createdDevice = req.CreateDevice();
        Assert.Multiple(() =>
        {
            Assert.That(createdDevice.Name, Is.EqualTo("TestName"));
            Assert.That(createdDevice.CpuUtilizations, Is.Empty);
            Assert.That(createdDevice.MemoryOccupies, Is.Empty);
        });
    }
}