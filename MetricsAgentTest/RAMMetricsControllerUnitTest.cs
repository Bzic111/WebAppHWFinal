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
        var fromTime = DateTime.FromSeconds(0);
        var toTime = DateTime.FromSeconds(100);

        var result = controller.GetRAMMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
