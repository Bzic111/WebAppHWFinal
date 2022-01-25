using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class RAMMetricsController : ControllerBase
{
    [HttpGet("ram/available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRAMMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }
}
