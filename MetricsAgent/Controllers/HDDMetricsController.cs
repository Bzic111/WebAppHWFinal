using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    [HttpGet("hdd/left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        return Ok();
    }
}
