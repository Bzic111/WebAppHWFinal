using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics")]
[ApiController]
public class DotNetMetricsController : ControllerBase
{

    [HttpGet("dotnet/errors-count/from/{fromTime}/to/{toTime}")]
    public IActionResult GetDotNetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }
}
