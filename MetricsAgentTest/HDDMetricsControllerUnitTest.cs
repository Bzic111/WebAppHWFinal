using Microsoft.Extensions.Logging;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTest;

public class HDDMetricsControllerUnitTest
{
    private HDDMetricsController controller;
    private Mock<IHddMetricsRepository> mock;
    private Mock<ILogger<HDDMetricsController>> logger;
    public HDDMetricsControllerUnitTest()
    {
        logger = new Mock<ILogger<HDDMetricsController>>();
        mock = new();
        controller = new(mock.Object, logger.Object);
    }

    [Fact]
    public void GetHDDMetrics_ReturnsOk()
    {
        var fromTime = DateTime.Now - TimeSpan.FromDays(1);
        var toTime = DateTime.Now;

        var result = controller.GetHDDMetrics(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
