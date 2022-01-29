namespace MetricsManagerTest;

public class DotNetMetricsControllerUnitTest
{
    private DotNetMetricsController controller;
    private Mock<ILogger<DotNetMetricsController>> logger;
    public DotNetMetricsControllerUnitTest()
    {
        logger = new();
        controller = new(logger.Object);
    }

    [Fact]
    public void GetFilteredMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetFilteredMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
