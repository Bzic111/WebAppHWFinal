namespace MetricsManagerTest;

public class HDDMetricsControllerUnitTest
{
    private HDDMetricsController controller;
    private Mock<ILogger<HDDMetricsController>> logger;
    public HDDMetricsControllerUnitTest()
    {
        logger = new();
        controller = new(logger.Object);
    }

    [Fact]
    public void GetHDDMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
    [Fact]
    public void GetHDDMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
