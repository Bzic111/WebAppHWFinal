﻿using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsManager.Controllers;

namespace MetricsManagerTest;

public class HDDMetricsControllerUnitTest
{
    private HDDMetricsController controller;
    public HDDMetricsControllerUnitTest()
    {
        controller = new();
    }

    [Fact]
    public void GetHDDMetricsFromAgent_ReturnsOk()
    {
        var agentId = 1;
        var fromTime = DateTime.FromSeconds(0);
        var toTime = DateTime.FromSeconds(100);

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
    [Fact]
    public void GetHDDMetricsFromCluster_ReturnsOk()
    {
        var fromTime = DateTime.FromSeconds(0);
        var toTime = DateTime.FromSeconds(100);

        var result = controller.GetMetricsFromCluster(fromTime, toTime);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
