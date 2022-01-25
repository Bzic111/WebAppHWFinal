using MetricsManager.Other;
using Microsoft.AspNetCore.Http;
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
        return Ok();
    }
    [HttpPut("enable/{agentId}")]
    public IActionResult EnableAgentById([FromRoute] int agentId)
    {
        return Ok();
    }
    [HttpPut("disable/{agentId}")]
    public IActionResult DisableAgentById([FromRoute] int agentId)
    {
        return Ok();
    }
}
