using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTest;

public class RAMMetricsControllerUnitTest
{
    private RAMMetricsController controller;
    public RAMMetricsControllerUnitTest()
    {
        controller = new RAMMetricsController();
    }

    [Fact]
    public void GetRAMMetrics_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetRAMMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
