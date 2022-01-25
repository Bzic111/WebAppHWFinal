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
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetRAMMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
