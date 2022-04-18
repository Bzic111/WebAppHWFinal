namespace MetricsAgent.Controllers;

[Route("api/metrics/hdd")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    private readonly ILogger<HDDMetricsController> _logger;
    private IHddMetricsRepository _repository;
    private readonly IMapper _mapper;
    public HDDMetricsController(IHddMetricsRepository repo, ILogger<HDDMetricsController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repo;
    }

    #region Create

    [HttpPost("create")]
    public IActionResult Create([FromBody] HddMetricCreateRequest request)
    {
        _logger.LogInformation($"Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Create(new HddMetric
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
        var response = new AllHddMetricsResponse() { Metrics = new List<HddMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
        return Ok(response);
    }

    [HttpGet("left/filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllHddMetricsResponse() { Metrics = new List<HddMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
        return Ok(response);
    }

    #endregion

    #region Update

    [HttpPut("update")]
    public IActionResult UpdateMetric([FromQuery] int id, [FromBody] DotNetCreateRequest request)
    {
        _logger.LogInformation($"Update Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Update(new HddMetric()
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
