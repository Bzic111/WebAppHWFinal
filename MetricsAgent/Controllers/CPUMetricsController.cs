using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    [HttpGet("cpu/from/{fromTime}/to/{toTime}")]
    public IActionResult GetCPUMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }
}
