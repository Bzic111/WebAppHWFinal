namespace MetricsAgent.Controllers;

[Route("api/metrics/ram")]
[ApiController]
public class RAMMetricsController : ControllerBase
{
    private readonly ILogger<RAMMetricsController> _logger;
    private IRamMetricsRepository _repository;
    private readonly IMapper _mapper;
    public RAMMetricsController(IRamMetricsRepository repo, ILogger<RAMMetricsController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repo;
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] RamMetricCreateRequest request)
    {
        _logger.LogInformation($"Request: \nTime = {request.Time}\nValue = {request.Value}");
        _repository.Create(new RamMetric
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
        var response = new AllRamMetricsResponse() { Metrics = new List<RamMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
        return Ok(response);
    }

    [HttpGet("available/filter")]
    public IActionResult GetFilteredMetrics([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
    {
        var metrics = _repository.GetByTimePeriod(fromTime, toTime);
        _logger.LogInformation($"GetFilteredData()\nFrom Date = {fromTime}\nTo Dota = {toTime}\n returns = {(metrics is not null ? "list" : "null")}");
        var response = new AllRamMetricsResponse() { Metrics = new List<RamMetricDto>() };
        foreach (var metric in metrics!)
            response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
        return Ok(response);
    }
}
