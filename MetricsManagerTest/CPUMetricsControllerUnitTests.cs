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
        //controller = new CPUMetricsController();
    }

    //[Fact]
    public void GetMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    //[Fact]
    public void GetMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
