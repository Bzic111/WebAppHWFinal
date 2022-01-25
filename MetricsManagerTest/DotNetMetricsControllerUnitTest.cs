using Microsoft.AspNetCore.Mvc;
using System;

using Xunit;
using MetricsManager.Controllers;

namespace MetricsManagerTest;

public class DotNetMetricsControllerUnitTest
{
    private DotNetMetricsController controller;
    public DotNetMetricsControllerUnitTest()
    {
        controller = new();
    }

    [Fact]
    public void GetDotNetMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetDotNetMetricsFromCluster_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
