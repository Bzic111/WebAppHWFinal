using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTest;

public class NetworkMetricsControllerUnitTest
{
    private NetworkMetricsController controller;
    public NetworkMetricsControllerUnitTest()
    {
        controller = new();
    }
    [Fact]

    public void GetNetworkMetrics_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetNetworkMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
