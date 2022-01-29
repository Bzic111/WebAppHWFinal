namespace MetricsAgent.Controllers;

[Route("api/metrics/network")]
[ApiController]
public class NetworkMetricsController : ControllerBase
{
    private readonly ILogger<NetworkMetricsController> _logger;
    private INetworkRepository _repository;
    public NetworkMetricsController(INetworkRepository repo, ILogger<NetworkMetricsController> logger)
    {
        _logger = logger;
        _repository = repo;
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] NetworkCreateRequest request)
    {
        _logger.LogInformation($"Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Create(new NetworkMetric
        {
            Time = request.Time,
            Value = request.Value
        });
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var metrics = _repository.GetAll();
        _logger.LogInformation($"GetAll() returns {(metrics is not null ? "list" : "null")}");
        var response = new AllNetworkMetricsResponse()
        {
            Metrics = new List<NetworkMetricDto>()
        };
        foreach (var metric in metrics!)
        {
            response.Metrics.Add(new NetworkMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
    
    [HttpGet("from/filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllNetworkMetricsResponse() { Metrics = new List<NetworkMetricDto>() };
        foreach (var metric in metrics!)
        {
            response.Metrics.Add(new NetworkMetricDto
            {
                Time = metric.Time,
                Value = metric.Value,
                Id = metric.Id
            });
        }
        return Ok(response);
    }
}
