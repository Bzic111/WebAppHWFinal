using Microsoft.AspNetCore.Mvc;
using System;

using Xunit;
using MetricsManager.Controllers;

namespace MetricsManagerTest;

public class CPUMetricsControllerUnitTests
{
    private CPUMetricsController controller;
    public CPUMetricsControllerUnitTests()
    {
        controller = new CPUMetricsController();
    }

    [Fact]
    public void GetMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetMetricsFromCluster_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
