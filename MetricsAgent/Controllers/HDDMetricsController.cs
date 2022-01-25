using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    [HttpGet("hdd/left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }
}
