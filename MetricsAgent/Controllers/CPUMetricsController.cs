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
    private ICpuMetricsRepository _cpuMetricsRepository;
    public CPUMetricsController(ICpuMetricsRepository repo)
    {
        _cpuMetricsRepository = repo;
    }
    [HttpPost("create")]

    public IActionResult Create([FromBody] CpuMetricCreateRequest request)
    {
        _cpuMetricsRepository.Create(new CpuMetric
        {
            Time = request.Time,
            Value = request.Value
        });
        return Ok();
    }
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var metrics = _cpuMetricsRepository.GetAll();
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
    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetCPUMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        return Ok();
    }
}
