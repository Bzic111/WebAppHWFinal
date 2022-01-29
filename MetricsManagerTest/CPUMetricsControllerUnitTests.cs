global using Moq;
global using Microsoft.AspNetCore.Mvc;
global using Xunit;
global using System;
global using MetricsManager.Controllers;
global using Microsoft.Extensions.Logging;

namespace MetricsManagerTest;

public class CPUMetricsControllerUnitTests
{
    private CPUMetricsController controller;
    private Mock<ILogger<CPUMetricsController>> mockLogger;

    public CPUMetricsControllerUnitTests()
    {
        mockLogger = new Mock<ILogger<CPUMetricsController>>();
        controller = new(mockLogger.Object);
    }

    [Fact]
    public void GetMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
