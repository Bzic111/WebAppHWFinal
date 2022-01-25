using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTest;

public class CPUMetricsControllerUnitTests
{
    private CPUMetricsController controller;
    public CPUMetricsControllerUnitTests()
    {
        controller = new CPUMetricsController();
    }

    [Fact]
    public void GetCPUMetrics_ReturnsOk()
    {
        var fromTime = TimeSpan.FromSeconds(0);
        var toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetCPUMetrics(fromTime, toTime);

        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}