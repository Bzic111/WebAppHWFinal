using MetricsManager.Other;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly ILogger<AgentsController> _logger;
    public AgentsController(ILogger<AgentsController> logger)
    {
        _logger = logger;
        _logger.LogInformation("Logger added to AgentsController");
    }

    [HttpPost("register")]
    public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
    {
        _logger.LogInformation($"Register Agent:\n{agentInfo.AgentId}\n{agentInfo.AgentAddress}");
        return Ok();
    }
    [HttpPut("enable/{agentId}")]
    public IActionResult EnableAgentById([FromRoute] int agentId)
    {
        _logger.LogInformation($"Enable Agent{agentId}");
        return Ok();
    }
    [HttpPut("disable/{agentId}")]
    public IActionResult DisableAgentById([FromRoute] int agentId)
    {
        _logger.LogInformation($"Disable Agent{agentId}");
        return Ok();
    }
}
