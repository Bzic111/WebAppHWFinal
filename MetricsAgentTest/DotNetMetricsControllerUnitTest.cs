using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTest;

public class DotNetMetricsControllerUnitTest
{
    private DotNetMetricsController controller;
    public DotNetMetricsControllerUnitTest()
    {
        controller = new();
    }

    [Fact]
    public void GetDotNetMetrics_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetDotNetMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
