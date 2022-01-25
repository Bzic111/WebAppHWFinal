using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTest;

public class HDDMetricsControllerUnitTest
{
    private HDDMetricsController controller;
    public HDDMetricsControllerUnitTest()
    {
        controller = new();
    }

    [Fact]
    public void GetHDDMetrics_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetHDDMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
