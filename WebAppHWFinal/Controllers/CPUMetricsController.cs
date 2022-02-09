using MetricsManager.DAL.Repositoryes;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    private readonly ILogger<CPUMetricsController> _logger;

    public CPUMetricsController(ILogger<CPUMetricsController> logger)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
    }
    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        _logger.LogInformation($"GetMetricsFromAgent {agentId}\nFrom {fromTime}\nTo {toTime}");
        return Ok();
    }
    [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromCluster([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        _logger.LogInformation($"GetMetricsFromCluster \nFrom {fromTime}\nTo {toTime}");
        return Ok();
    }
}
