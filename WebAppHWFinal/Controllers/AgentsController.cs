using MetricsManager.Other;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[Route("api/agents")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly ILogger<AgentsController> _logger;
    private readonly IAgentsRepository _repository;

    public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository)
    {
        _logger = logger;
        _repository = repository;
        _logger.LogInformation("Logger added to AgentsController");
    }
    #region Create

    [HttpPost("register")]
    public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
    {
        _logger.LogInformation($"Register Agent:\n{agentInfo.AgentId}\n{agentInfo.AgentAddress}");
        _repository.Create(agentInfo.AgentAddress);
        return Ok();
    }

    #endregion

    #region Read

    #endregion

    #region Update

    #endregion

    #region Delete

    #endregion


    [HttpGet("id/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        return Ok(_repository.GetById(id));
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
