using Microsoft.Extensions.Logging;
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
    private Mock<IDotNetRepository> mock;
    private Mock<ILogger<DotNetMetricsController>> logger;
    public DotNetMetricsControllerUnitTest()
    {
        logger = new Mock<ILogger<DotNetMetricsController>>();
        mock = new Mock<IDotNetRepository>();
        controller = new(mock.Object,logger.Object);
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
