using WebAppHWFinal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTest;

public class NetworkMetricsControllerUnitTest
{
    private NetworkMetricsController controller;
    public NetworkMetricsControllerUnitTest()
    {
        controller = new();
    }

    [Fact]
    public void GetNetworkMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetNetworkMetricsFromCluster_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
