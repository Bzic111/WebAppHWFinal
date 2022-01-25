using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class NetworkMetricsController : ControllerBase
{
    [HttpGet("network/from/{fromTime}/to/{toTime}")]
    public IActionResult GetNetworkMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        return Ok();
    }
}
