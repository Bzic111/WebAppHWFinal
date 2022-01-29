using MetricsAgent.Controllers;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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
        controller = new(mock.Object, logger.Object);
    }

    [Fact]
    public void Create_ShouldCall_Create_From_Repository()
    {
        mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
        var result = controller.Create(new MetricsAgent.Requests.DotNetCreateRequest { Time = DateTime.Now, Value = 50 });
        mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
    }

    [Fact]
    public void GetAll_Should_GetAll_From_Repository()
    {
        var responseList = new List<DotNetMetric>()
            {
                new DotNetMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new DotNetMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new DotNetMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        mock.Setup(repository => repository.GetAll()).Returns(responseList);
        var result = controller.GetAll();
        mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }

    [Fact]
    public void GetFilteredByDate()
    {
        var responseList = new List<DotNetMetric>()
            {
                new DotNetMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new DotNetMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new DotNetMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        DateTime from = new DateTime(2022, 01, 28);
        DateTime to = new DateTime(2022, 01, 30);
        mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(responseList);
        var result = controller.GetFilteredMetrics(from, to);
        mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
    }
}
