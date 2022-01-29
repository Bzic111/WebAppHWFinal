namespace MetricsAgent.Controllers;

[Route("api/metrics/network")]
[ApiController]
public class NetworkMetricsController : ControllerBase
{
    private readonly ILogger<NetworkMetricsController> _logger;
    private INetworkRepository _repository;
    private readonly IMapper _mapper;
    
    public NetworkMetricsController(INetworkRepository repo, ILogger<NetworkMetricsController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repo;
    }

    #region Create

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

    #endregion

    #region Read

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var metrics = _repository.GetAll();
        _logger.LogInformation($"GetAll() returns {(metrics is not null ? "list" : "null")}");
        var response = new AllNetworkMetricsResponse() { Metrics = new List<NetworkMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
        return Ok(response);
    }

    [HttpGet("from/filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllNetworkMetricsResponse() { Metrics = new List<NetworkMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
        return Ok(response);
    }

    #endregion

    #region Update

    [HttpPut("update")]
    public IActionResult UpdateMetric([FromQuery] int id, [FromBody] CpuMetricCreateRequest request)
    {
        _logger.LogInformation($"Update Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Update(new NetworkMetric()
        {
            Id = id,
            Value = request.Value,
            Time = request.Time
        });
        return Ok("Updated");
    }

    #endregion

    #region Delete

    [HttpDelete("delete")]
    public IActionResult DeleteMetric([FromQuery] int id)
    {
        _repository.Delete(id);
        return Ok("Deleted");
    }

    #endregion
}
