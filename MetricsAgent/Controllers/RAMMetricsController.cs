using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class RAMMetricsController : ControllerBase
{
    [HttpGet("ram/available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRAMMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        return Ok();
    }
}
