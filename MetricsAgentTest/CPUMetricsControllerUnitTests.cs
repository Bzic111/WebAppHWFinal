using MetricsAgent.Controllers;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
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
        mock.Setup(repository => repository.GetAll()).Verifiable();
        var result = controller.GetAll();
        mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }
}