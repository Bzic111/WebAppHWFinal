using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Http;
using MetricsAgent.Repositoryes;

namespace MetricsAgent.Controllers;

[Route("api/metrics/hdd")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    private readonly ILogger<HDDMetricsController> _logger;
    private IHddMetricsRepository _repository;
    public HDDMetricsController(IHddMetricsRepository repo, ILogger<HDDMetricsController> logger)
    {
        _logger = logger;
        _repository = repo;
    }
    
    [HttpPost("create")]
    public IActionResult Create([FromBody] HddMetricCreateRequest request)
    {
        _logger.LogInformation($"Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Create(new HddMetric
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
        _logger.LogInformation($"GetAll() returns {(metrics is not null ? "list" : "null")}");
        var response = new AllHddMetricsResponse()
        {
            Metrics = new List<HddMetricDto>()
        };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new HddMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
    
    [HttpGet("left/filter}")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllHddMetricsResponse() { Metrics = new List<HddMetricDto>() };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new HddMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
}
