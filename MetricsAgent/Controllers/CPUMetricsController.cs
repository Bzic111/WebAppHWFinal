using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Http;
using MetricsAgent.Repositoryes;

namespace MetricsAgent.Controllers;

[Route("api/metrics/cpu")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    private ICpuMetricsRepository _repository;
    private readonly ILogger<CPUMetricsController> _logger;
    public CPUMetricsController(ICpuMetricsRepository repo, ILogger<CPUMetricsController> logger)
    {
        _logger = logger;
        _repository = repo;
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] CpuMetricCreateRequest request)
    {
        _repository.Create(new CpuMetric
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
        var response = new AllCpuMetricsResponse()
        {
            Metrics = new List<CpuMetricDto>()
        };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new CpuMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
    [HttpGet("filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {        
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        var response = new AllCpuMetricsResponse()
        {
            Metrics = new List<CpuMetricDto>()
        };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new CpuMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
}
