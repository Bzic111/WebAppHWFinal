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
        var fromTime = DateTime.FromSeconds(0);
        var toTime = DateTime.FromSeconds(100);

        var result = controller.GetNetworkMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
