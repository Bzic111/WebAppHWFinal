using MetricsAgent.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgentTest;

public class RAMMetricsControllerUnitTest
{
    private RAMMetricsController controller;
    private Mock<IRamMetricsRepository> mock;
    private Mock<ILogger<RAMMetricsController>> logger;

    public RAMMetricsControllerUnitTest()
    {
        mock = new Mock<IRamMetricsRepository>();
        logger = new Mock<ILogger<RAMMetricsController>>();
        controller = new RAMMetricsController(mock.Object, logger.Object);
    }

    [Fact]
    public void Create_ShouldCall_Create_From_Repository()
    {
        mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();
        var result = controller.Create(new MetricsAgent.Requests.RamMetricCreateRequest { Time = DateTime.Now, Value = 50 });
        mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
    }

    [Fact]
    public void GetAll_Should_GetAll_From_Repository()
    {
        var responseList = new List<RamMetric>()
            {
                new RamMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new RamMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new RamMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        mock.Setup(repository => repository.GetAll()).Returns(responseList);
        var result = controller.GetAll();
        mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }

    [Fact]
    public void GetFilteredByDate()
    {
        var responseList = new List<RamMetric>()
            {
                new RamMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new RamMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new RamMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        DateTime from = new DateTime(2022, 01, 28);
        DateTime to = new DateTime(2022, 01, 30);
        mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(responseList);
        var result = controller.GetFilteredMetrics(from, to);
        mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());

    }
}
