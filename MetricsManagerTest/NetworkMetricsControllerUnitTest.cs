namespace MetricsManagerTest;

public class NetworkMetricsControllerUnitTest
{
    private NetworkMetricsController controller;
    private Mock<ILogger<NetworkMetricsController>> logger;
    public NetworkMetricsControllerUnitTest()
    {
        logger = new();
        controller = new(logger.Object);
    }

    [Fact]
    public void GetNetworkMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;
        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetNetworkMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
