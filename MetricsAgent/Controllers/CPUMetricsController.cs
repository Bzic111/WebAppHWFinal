namespace MetricsAgent.Controllers;

[Route("api/metrics/cpu")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    private ICpuMetricsRepository _repository;
    private readonly ILogger<CPUMetricsController> _logger;
    private readonly IMapper _mapper;
    
    public CPUMetricsController(ICpuMetricsRepository repo, ILogger<CPUMetricsController> logger, IMapper mapper)
    {
        _logger = logger;
        _repository = repo;
        _mapper = mapper;
    }

    #region Create

    [HttpPost("create")]
    public IActionResult Create([FromBody] CpuMetricCreateRequest request)
    {
        _logger.LogInformation($"Create Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Create(new CpuMetric
        {
            Time = request.Time,
            Value = request.Value
        });
        return Ok("Created");
    }

    #endregion

    #region Read

    [HttpGet("usage/all")]
    public IActionResult GetAll()
    {
        IList<CpuMetric> metrics = _repository.GetAll();
        _logger.LogInformation($"GetAll() returns {(metrics is not null ? "list" : "null")}");
        var response = new AllCpuMetricsResponse() { Metrics = new List<CpuMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
        return Ok(response);
    }

    [HttpGet("usage/filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        IList<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllCpuMetricsResponse() { Metrics = new List<CpuMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
        return Ok(response);
    }

    #endregion

    #region Update

    [HttpPut("update")]
    public IActionResult UpdateMetric([FromQuery] int id,[FromBody] CpuMetricCreateRequest request)
    {
        _logger.LogInformation($"Update Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Update(new CpuMetric()
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
