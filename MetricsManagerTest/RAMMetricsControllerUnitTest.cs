using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsManager.Controllers;

namespace MetricsManagerTest;

public class RAMMetricsControllerUnitTest
{
    private RAMMetricsController controller;
    public RAMMetricsControllerUnitTest()
    {
        controller = new RAMMetricsController();
    }

    [Fact]
    public void GetRAMMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.FromSeconds(0);
        var toTime = DateTime.FromSeconds(100);

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetRAMMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.FromSeconds(0);
        var toTime = DateTime.FromSeconds(100);

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
