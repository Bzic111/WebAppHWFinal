namespace MetricsAgent.Controllers;

[Route("api/metrics/dotnet")]
[ApiController]
public class DotNetMetricsController : ControllerBase
{
    private IDotNetRepository _repository;
    private readonly ILogger<DotNetMetricsController> _logger;
    private readonly IMapper _mapper;
    public DotNetMetricsController(IDotNetRepository repo, ILogger<DotNetMetricsController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repo;
    }

    #region Create

    [HttpPost("create")]
    public IActionResult Create([FromBody] DotNetCreateRequest request)
    {
        _logger.LogInformation($"Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Create(new DotNetMetric
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
        var response = new AllDotNetMetricsResponse() { Metrics = new List<DotNetMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
        return Ok(response);
    }

    [HttpGet("errors-count/filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllDotNetMetricsResponse() { Metrics = new List<DotNetMetricDto>() };
        foreach (var metric in metrics!) 
            response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
        return Ok(response);

    }

    #endregion

    #region Update

    [HttpPut("update")]
    public IActionResult UpdateMetric([FromQuery] int id, [FromBody] DotNetCreateRequest request)
    {
        _logger.LogInformation($"Update Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Update(new DotNetMetric()
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
