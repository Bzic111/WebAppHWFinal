using MetricsAgent.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgentTest;

public class NetworkMetricsControllerUnitTest
{
    private NetworkMetricsController controller;
    private Mock<INetworkRepository> mock;
    private Mock<ILogger<NetworkMetricsController>> mockLogger;
    
    public NetworkMetricsControllerUnitTest()
    {
        mockLogger = new Mock<ILogger<NetworkMetricsController>>();
        mock = new Mock<INetworkRepository>();
        controller = new NetworkMetricsController(mock.Object, mockLogger.Object);
    }

    [Fact]
    public void Create_ShouldCall_Create_From_Repository()
    {
        mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
        var result = controller.Create(new MetricsAgent.Requests.NetworkCreateRequest { Time = DateTime.Now, Value = 50 });
        mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
    }

    [Fact]
    public void GetAll_Should_GetAll_From_Repository()
    {
        var responseList = new List<NetworkMetric>()
            {
                new NetworkMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new NetworkMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new NetworkMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        mock.Setup(repository => repository.GetAll()).Returns(responseList);
        var result = controller.GetAll();
        mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }

    [Fact]
    public void GetFilteredByDate()
    {
        var responseList = new List<NetworkMetric>()
            {
                new NetworkMetric(){ Id = 1,Value = 50,Time = DateTime.Now},
                new NetworkMetric(){ Id = 2,Value = 51,Time = DateTime.Now},
                new NetworkMetric(){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        DateTime from = new DateTime(2022, 01, 28);
        DateTime to = new DateTime(2022, 01, 30);
        mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(responseList);
        var result = controller.GetFilteredMetrics(from, to);
        mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
    }
}
