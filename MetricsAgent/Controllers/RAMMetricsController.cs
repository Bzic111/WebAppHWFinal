using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Http;
using MetricsAgent.Repositoryes;
namespace MetricsAgent.Controllers;

[Route("api/metrics/ram")]
[ApiController]
public class RAMMetricsController : ControllerBase
{
    private readonly ILogger<RAMMetricsController> _logger;
    private IRamMetricsRepository _repository;
    public RAMMetricsController(IRamMetricsRepository repo, ILogger<RAMMetricsController> logger)
    {
        _logger = logger;
        _repository = repo;
    }
    [HttpPost("create")]
    public IActionResult Create([FromBody] RamMetricCreateRequest request)
    {
        _repository.Create(new RamMetric
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
        var response = new AllRamMetricsResponse()
        {
            Metrics = new List<RamMetricDto>()
        };
        foreach (var metric in metrics)
        {
            response.Metrics.Add(new RamMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }

    [HttpGet("available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRAMMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        return Ok();
    }
}
