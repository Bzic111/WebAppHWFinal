using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NetworkMetricsController : ControllerBase
{
    private readonly ILogger<NetworkMetricsController> _logger;

    public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
    {
        _logger = logger;
        _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
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
