using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Http;
using MetricsAgent.Repositoryes;

namespace MetricsAgent.Controllers;

[Route("api/metrics/network")]
[ApiController]
public class NetworkMetricsController : ControllerBase
{
    private readonly ILogger<NetworkMetricsController> _logger;
    private INetworkRepository _repository;
    public NetworkMetricsController(INetworkRepository repo, ILogger<NetworkMetricsController> logger)
    {
        _logger = logger;
        _repository = repo;
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] NetworkCreateRequest request)
    {
        _repository.Create(new NetworkMetric
        {
            Time = request.Time,
            Value = request.Value
        });
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var metrics = _repository.GetAll();
        var response = new AllNetworkMetricsResponse()
        {
            Metrics = new List<NetworkMetricDto>()
        };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new NetworkMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
    [HttpGet("from/filter")]
    public IActionResult GetNetworkMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        var response = new AllNetworkMetricsResponse() { Metrics = new List<NetworkMetricDto>() };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new NetworkMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
}
