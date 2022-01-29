using MetricsAgent.DTO;
using MetricsAgent.Controllers;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
//using MetricsAgent.DAL;


namespace MetricsAgentTest;

public class CPUMetricsControllerUnitTests
{
    private CPUMetricsController controller;
    private Mock<ICpuMetricsRepository> mock;
    private Mock<ILogger<CPUMetricsController>> mockLogger;
    public CPUMetricsControllerUnitTests()
    {
        mockLogger = new Mock<ILogger<CPUMetricsController>>();
        mock = new Mock<ICpuMetricsRepository>();
        controller = new CPUMetricsController(mock.Object, mockLogger.Object);
    }

    [Fact]
    public void Create_ShouldCall_Create_From_Repository()
    {
        mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
        var result = controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = DateTime.Now, Value = 50 });
        mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
    }

    [Fact]
    public void GetAll_Should_GetAll_From_Repository()
    {
        var responseList = new List<CpuMetric>()
            {
                new CpuMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new CpuMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new CpuMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        mock.Setup(repository => repository.GetAll()).Returns(responseList);
        var result = controller.GetAll();
        mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }

    [Fact]
    public void GetFilteredByDate()
    {
        var responseList = new List<CpuMetric>()
            {
                new CpuMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new CpuMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new CpuMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        DateTime from = new DateTime(2022, 01, 28);
        DateTime to = new DateTime(2022, 01, 30);
        mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(responseList);
        var result = controller.GetFilteredMetrics(from, to);
        mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
    }
}