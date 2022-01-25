using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTest;

public class DotNetMetricsControllerUnitTest
{
    private DotNetMetricsController controller;
    private Mock<DotNetMetricsController> mock;
    public DotNetMetricsControllerUnitTest()
    {
        mock = new Mock<DotNetMetricsController>();
        controller = new();
    }

    [Fact]
    public void GetDotNetMetrics_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetDotNetMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
